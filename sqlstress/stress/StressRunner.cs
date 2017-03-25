using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace sqlstress
{
    public class StressRunner
    {
        public StressScheme Scheme {get; private set;}
        private SchemeFeeder SchemeRunner;
        public SQLStressEngine Engine;
        public perfcount Counter;

        //public perfcount PefCounter {get; set;}
        public Timer TimeCunter = new Timer();

        public EventHandler OnFinished;

        //private StressEngine.WorkerCount[] WorkStatus = new StressEngine.WorkerCount[5];
        //private int StatusIndex = 0;

        private SQLStressEngine.WorkerCount WorkCountData0 = SQLStressEngine.WorkerCount.Empty;
        private SQLStressEngine.WorkerCount WorkCountData1 = SQLStressEngine.WorkerCount.Empty;

        private object datasync = new object();
        /*
        public StressRunner(string _Scheme)
        {
            //Scheme = Utils.XmlSerializerObject.ObjFromXmlFile<StressScheme>(StressScheme.GetSchemeFile(_Scheme));
            Scheme = new StressScheme(_Scheme);
            SchemeRunner = new SchemeFeeder(Scheme);
            Engine = new SQLStressEngine(new SQLStressEngine.EngineOption() { readresult = Scheme.run_withresult, workerscount = Scheme.run_threads }, Scheme.dbsettings, SchemeRunner);
            TimeCunter.Interval = 1000;            
            TimeCunter.Enabled = false;
            TimeCunter.Elapsed += OnTimer;
        }
        */
        public StressRunner(StressScheme _Scheme)
        {
            //Scheme = Utils.XmlSerializerObject.ObjFromXmlFile<StressScheme>(StressScheme.GetSchemeFile(_Scheme));
            Scheme = _Scheme;
            SchemeRunner = new SchemeFeeder(Scheme);
            Engine = new SQLStressEngine(new SQLStressEngine.EngineOption() { readresult = Scheme.run_withresult, workerscount = Scheme.run_threads }, Scheme.dbsettings, SchemeRunner);
            TimeCunter.Interval = 1000;
            TimeCunter.Enabled = false;
            TimeCunter.Elapsed += OnTimer;
        }

        public void OnTimer(object sender, ElapsedEventArgs e)
        {
            //WorkStatus[StatusIndex % 5] = Engine.GlobalWrokCount;
            if (Engine.GlobalWrokCount.timestamp == 0) return;
            //if (WorkCountData1.timestamp == Engine.GlobalWrokCount.timestamp) return;
            //if (WorkCountData1.donecount - WorkCountData1.errorcount == Engine.GlobalWrokCount.donecount - Engine.GlobalWrokCount.errorcount) return;
            //if (WorkCountData1.time_total == Engine.GlobalWrokCount.time_total) return;

            lock (datasync)
            {
                WorkCountData0 = WorkCountData1;
                WorkCountData1 = Engine.GlobalWrokCount;
            }

            Counter[perfcount.Counter_Requests].counter.RawValue = Perfmon_Request_PS;
            Counter[perfcount.Counter_ReqDone].counter.RawValue = Perfmon_ReqDone_PS;
            Counter[perfcount.Counter_ReqTimeAvg].counter.RawValue = (long)Math.Round(Perfmon_Time_PD);

            if (DateTime.Now.Second % 10 == 0)
            {
                Counter[perfcount.Counter_Requests].counter.NextSample();
                Counter[perfcount.Counter_ReqDone].counter.NextSample();
                Counter[perfcount.Counter_ReqTimeAvg].counter.NextSample();
            }
        }

        public void StartRun()
        {
            //SchemeRunner.prepare();
            //SchemeRunner.FeedInit();
            Engine.onWorkEvent = OnEnginWorking;
            //Engine.onNeedFeed = SchemeRunner.feednext;
            if (Engine.OnWorkEnd == null) Engine.OnWorkEnd += OnFinished;
            TimeCunter.Enabled = true;
            Engine.StartWork();            
        }

        public void StopRun()
        {
            Engine.StopWork();
            //SchemeRunner.FeedFinish();
            TimeCunter.Enabled = false;

            TimeCunter.Enabled = false;
            WorkCountData0 = SQLStressEngine.WorkerCount.Empty;
            WorkCountData1 = SQLStressEngine.WorkerCount.Empty;
        }

        public long Perfmon_ReqDone_PS
        {
            get 
            {
                lock (datasync)
                {
                    if (WorkCountData1.timestamp == WorkCountData0.timestamp) return 0;
                    return 10000 * 1000 * ((WorkCountData1.donecount - WorkCountData1.errorcount) - (WorkCountData0.donecount - WorkCountData0.errorcount)) / (WorkCountData1.timestamp - WorkCountData0.timestamp);
                }
            }
        }

        public double Perfmon_Time_PD
        {
            get
            {
                lock (datasync)
                {
                    if (WorkCountData1.timestamp == WorkCountData0.timestamp) return 0;
                    if ((WorkCountData1.donecount - WorkCountData1.errorcount) - (WorkCountData0.donecount - WorkCountData0.errorcount) == 0) return 0;
                    return 1.0 * (WorkCountData1.time_total - WorkCountData0.time_total) / ((WorkCountData1.donecount - WorkCountData1.errorcount) - (WorkCountData0.donecount - WorkCountData0.errorcount));
                }
            }
        }

        public long Perfmon_Request_PS
        {
            get
            {
                lock (datasync)
                {
                    if (WorkCountData1.timestamp == WorkCountData0.timestamp) return 0;
                    return (10000 * 1000 * (WorkCountData1.donecount - WorkCountData0.donecount) / (WorkCountData1.timestamp - WorkCountData0.timestamp));
                }
            }
        }

        public void OnEnginWorking(ref SQLStressEngine.WorkerInfo workerinfo, int EventId)
        {
            if (EventId == 0)
            {

            }
            else if (EventId > 0)
            {

            }
        }
    }
}
