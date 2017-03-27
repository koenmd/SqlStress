using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Threading;
using System.Diagnostics;


namespace sqlstress
{
    public class DbRunner
    {
        private IDbEngine Engine;
        private DbConnection Connection;
        private DbCommand Command;
        private DbDataAdapter DataAdapter;
        private DbEngineSetting Settings;
        private Stopwatch Timecounter = new Stopwatch();

        public DbParamTranslater ParamTranslater { get; set; } = DbParamTranslater.DefaultTranslater;
        public long TimeElapsed { get; private set; } = 0;

        public DbRunner(DbEngineSetting dbsettings)
        {
            Settings = dbsettings;
            Engine = Settings.CreateEngine();

            Connection = Engine.NewConnection(Settings.ConnectString);
            Command = Engine.NewCommand(); // new DBCommand(dbsettings);
            Command.Connection = Connection;
            try
            {
                Connection.Open();
            }
            catch (System.Exception ex)
            {
                Utils.Logger.Trace(ex, true);
            }
        }

        ~DbRunner()
        {
            if (Connection != null && (Connection.State == ConnectionState.Open))
            {
                //Connection.Connection.Close();
            }
        }

        private DbParamType getParamType(string ParamName, string ParamValue)
        {
            return ParamTranslater.Translate(ParamName, ParamValue);
        }

        public bool Run_WithResult(string sql, Dictionary<string, string> parameters, bool withparam)
        {
            try
            {
                Timecounter.Restart();

                Command.CommandText = sql;
                if (withparam)
                {
                    Command.Parameters.Clear();
                    foreach (KeyValuePair<string, string> param in parameters)
                    {
                        DbParameter p = Engine.NewParameter(param.Key, param.Value, getParamType(param.Key, param.Value));
                        Command.Parameters.Add(p);
                    }
                    //Command.Command.Prepare();
                }

                DbDataReader reader = Command.ExecuteReader();
                try
                {
                    do
                    {
                        while (reader.Read())
                        {
                            //grab the first column to force the row down the pipe
                            object x = reader[0];
                        }

                    } while (reader.NextResult());
                }
                catch (System.Exception ex)
                {
                    Utils.Logger.Trace(ex, false);
                }
                finally
                {
                    reader.Close();
                }

                Timecounter.Stop();
                this.TimeElapsed = Timecounter.ElapsedMilliseconds;

                return true;
            }
            catch (ThreadAbortException)
            {
                return false;
            }
            catch (System.Exception ex)
            {
                Utils.Logger.Trace(new Exception(sql));
                Utils.Logger.Trace(ex, false);
                return false;
            }
        }

        public bool Run_NoResult(string sql, Dictionary<string, string> parameters, bool withparam)
        {
            try
            {
                Timecounter.Restart();
                Command.CommandText = sql;
                if (withparam)
                {
                    Command.Parameters.Clear();
                    foreach (KeyValuePair<string, string> param in parameters)
                    {
                        DbParameter p = Engine.NewParameter(param.Key, param.Value, getParamType(param.Key, param.Value));
                        Command.Parameters.Add(p);
                    }
                    //Command.Command.Prepare();
                }

                Command.ExecuteNonQuery();
                Timecounter.Stop();
                this.TimeElapsed = Timecounter.ElapsedMilliseconds;

                return true;
            }
            catch (ThreadAbortException)
            {
                return false;
            }
            catch (System.Exception ex)
            {
                Utils.Logger.Trace(new Exception(sql));
                Utils.Logger.Trace(ex, false);
                return false;
            }
        }

        public DataTable Run_WithResult(string sql)
        {
            //Timecounter.Restart();
            DataTable result = new DataTable();
            try
            {
                DataAdapter = Engine.NewDataAdapter(Settings.ConnectString, sql); //new DBDataAdapter(Settings, sql);
                DataAdapter.Fill(result);
            }
            catch (System.Exception ex)
            {
                Utils.Logger.Trace(new Exception(sql));
                Utils.Logger.Trace(ex);
            }

            //Timecounter.Stop();
            this.TimeElapsed = Timecounter.ElapsedMilliseconds;
            return result;
        }
    }
}
