using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics;
using System.Data.Common;


namespace sqlstress
{
    public interface IDbStressFeeder
    {
        void FeederInit(DbStressEngine engin);
        string[] ExecInit();
        string[] ExecFinished();        
        string[] FeedFirst();        
        bool FeedNext(int index, ref string sql, ref Dictionary<string, string> parameters);
    }

    public class DbStressEngine
    {
        public class EngineOption
        {
            public bool readresult;
            public int workerscount;
        }

        public struct WorkerCounter
        {
            public long timestamp;
            public long donecount;
            public long time_total;
            public long time_max;
            public long errorcount;
            public string lastjob;
            public readonly static WorkerCounter Empty = new WorkerCounter()
            {
                donecount = 0,
                time_total = 0,
                time_max = 0,
                errorcount = 0,
                lastjob = ""
            };
        }

        public class WorkerInfo
        {
            public DbRunner worker;
            public int index;
            public WorkerCounter workcount;
        }
                
        public EngineOption Option {get; private set;}
        public DbEngineSetting Settings { get; private set; }
        public IDbStressFeeder Feeder { get; private set; }

        public WorkerInfo[] Workers;
        public WorkerCounter GlobalWrokCount = WorkerCounter.Empty;
        private readonly Dictionary<WorkerInfo, Thread> ThreadPool = new Dictionary<WorkerInfo, Thread>();

        public delegate void delegateWorkerEvent(ref DbStressEngine.WorkerInfo workerinfo, int EventId);
        public delegateWorkerEvent onWorkEvent;
        public EventHandler OnWorkEnd;

        private Thread Monitor;
        private object datasync = new object();

        public DbStressEngine(EngineOption option, DbEngineSetting settings, IDbStressFeeder feeder)
        {
            Option = option;
            Settings = settings;
            Feeder = feeder;
            Trace.Assert(Feeder != null);
        }

        private void CreateWorkers()
        {
            Workers = new WorkerInfo[Option.workerscount];
            for (int i = 0; i < Option.workerscount; i++)
            {
                Workers[i] = new WorkerInfo()
                {
                    worker = new DbRunner(Settings),
                    index = i,
                    workcount = WorkerCounter.Empty
                };
                Workers[i].worker.ParamTranslater = new sqlparamvalueTranslater();
            }

            ThreadPool.Clear();
            foreach (WorkerInfo w in Workers)
            {
                ThreadPool.Add(w, new Thread(new ParameterizedThreadStart(WorkProc)));
            }
        }

        private void MonitorProc()
        {
            foreach (KeyValuePair<WorkerInfo, Thread> kv in ThreadPool)
            {
                kv.Value.Join();
            }

            if (OnWorkEnd != null)
            {
                OnWorkEnd(this, new EventArgs());
            }
        }

        private void StressInit()
        {
            DbRunner sqlr = new DbRunner(Settings);
            //string sql = "";
            foreach (string sql in Feeder.ExecInit())
            {
                sqlr.Run_NoResult(sql, null, false);
            } 
        }

        private void WorkersInit()
        {
            foreach (WorkerInfo workder in this.Workers)
            {
                DbRunner sqlr = workder.worker;
                //string sql = "";
                foreach (string sql in Feeder.FeedFirst())
                {
                    sqlr.Run_NoResult(sql, null, false);
                }
            }
        }

        public void StartWork()
        {
            GlobalWrokCount = WorkerCounter.Empty;

            string errormsg = "";
            if (!Settings.TestEngine(ref errormsg)) throw new Exception(errormsg);        

            Feeder.FeederInit(this);

            StressInit();

            CreateWorkers();

            WorkersInit();

            foreach (KeyValuePair<WorkerInfo, Thread> workerth in ThreadPool)
            {
                var wokerthread = workerth.Value;
                var workerwork = workerth.Key;
                wokerthread.Start(workerwork);
            }

            Monitor = new Thread(new ThreadStart(MonitorProc));
            Monitor.Start();
        }

        private void StressFinit()
        {
            DbRunner sqlr = new DbRunner(Settings);
            //string sql = "";
            foreach (string sql in Feeder.ExecFinished())
            {
                sqlr.Run_NoResult(sql, null, false);
            } 
        }

        public void StopWork()
        {
            try
            {
                foreach (KeyValuePair<WorkerInfo, Thread> kv in ThreadPool)
                {
                    kv.Value.Abort();
                }
            }
            catch(Exception)
            {
                //do nothing
            }

            StressFinit();
        }

        /*
        public bool WorkerFeed(WorkerInfo workerinfo, ref string sql, ref Dictionary<string, string> parameters)
        {
            if (onNeedFeed != null)
            {
                return onNeedFeed(workerinfo.index, ref sql, ref parameters);
            }
            return false;           
        }
         * */

        public bool WorkerWorks(WorkerInfo workerinfo)
        {
            string sql = "";
            Dictionary<string, string> parameters = null;

            if (Feeder.FeedNext(workerinfo.index, ref sql, ref parameters))
            {
                bool hasparam = (parameters != null);
                bool sqlresult = false;
                long timeelapsed = 0;

                if (Option.readresult)
                {
                    sqlresult = workerinfo.worker.Run_WithResult(sql, parameters, hasparam);
                }
                else
                {
                    sqlresult = workerinfo.worker.Run_NoResult(sql, parameters, hasparam);
                }

                workerinfo.workcount.donecount++;
                if (!sqlresult) workerinfo.workcount.errorcount++;
                timeelapsed = workerinfo.worker.TimeElapsed;
                if (timeelapsed > workerinfo.workcount.time_max) workerinfo.workcount.time_max = timeelapsed;
                if (sqlresult) workerinfo.workcount.time_total += timeelapsed;
                workerinfo.workcount.lastjob = hasparam ? string.Join(",", parameters.Values) : sql;

                lock (datasync)
                {
                    GlobalWrokCount.donecount++;
                    if (!sqlresult) GlobalWrokCount.errorcount++;
                    if (timeelapsed > GlobalWrokCount.time_max) GlobalWrokCount.time_max = timeelapsed;
                    if (sqlresult) GlobalWrokCount.time_total += timeelapsed;
                    GlobalWrokCount.timestamp = DateTime.Now.Ticks;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public void WorkProc(object workerinfoobj)
        {
            bool bcontinue = true;
            WorkerInfo workerinfo = (WorkerInfo)workerinfoobj;
            while (bcontinue)
            {
                try
                {
                    if (onWorkEvent != null) onWorkEvent(ref workerinfo, 0);
                    bcontinue = WorkerWorks(workerinfo);
                    if (onWorkEvent != null) onWorkEvent(ref workerinfo, 1);
                }
                catch (ThreadAbortException)
                {
                    bcontinue = false;
                    break;
                }
                catch (System.Exception ex)
                {
                    Utils.Logger.Trace(ex, false);
                };
            }
        }
    }
}
