using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.ConnectionUI;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.Common;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Reflection;

namespace sqlstress
{

    public class DbEngineHelper
    {
        public static Type[] GetEngineTypes()
        {
            List<Type> EngineTypes = new List<Type>();
            foreach (Assembly a in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in a.GetTypes())
                {
                    if (typeof(IdbEngine).IsAssignableFrom(t) && (t != typeof(IdbEngine)))
                    {
                        EngineTypes.Add(t);
                    }
                }
            }
            return EngineTypes.ToArray();
        }

        public static Type GetEngineType(Microsoft.Data.ConnectionUI.DataSource ds, Microsoft.Data.ConnectionUI.DataProvider dp)
        {
            Type[] EngineTypes = GetEngineTypes();
            foreach (Type t in EngineTypes)
            {
                IdbEngine tempEngine = (IdbEngine)t.Assembly.CreateInstance(t.FullName);
                DbEngineInfo info = tempEngine.GetInfo();

                if (info.datasource.Equals(ds) && info.dataprovider.DisplayName == dp.DisplayName)
                {
                    return t;
                }
            }

            return null;
        }

        public static Type GetEngineType(string name)
        {
            Type[] EngineTypes = GetEngineTypes();
            foreach (Type t in EngineTypes)
            {
                IdbEngine tempEngine = (IdbEngine)t.Assembly.CreateInstance(t.FullName);
                DbEngineInfo info = tempEngine.GetInfo();

                if (info.Name == name)
                {
                    return t;
                }
            }

            return null;
        }
    }

    [Serializable]
    [TypeConverter(typeof(XMLConvertor<DbEngineSetting>))]
    public class DbEngineSetting : IXmlSerializable, ICloneable
    {
        /*
        public enum ConnectionType
        {
            ODBC = 0,
            SQLCLIENT = 1
        }
        */

        public struct EngineSetting
        {
            public string Type;
            public string ConnectString;
        }

        public EngineSetting Setting = new EngineSetting();

        [Browsable(true)]
        public string ConnectString { get { return Setting.ConnectString; } }
        [Browsable(true)]
        public string Type { get { return Setting.Type; } }

        public DbEngineSetting()
        {

        }

        public void ShowWizard()
        {
            dbConnectonwiz.WizSetConnection(ref Setting);
        }

        public IdbEngine CreateEngine()
        {
            Type EngineType = DbEngineHelper.GetEngineType(Setting.Type);
            if (EngineType != null)
            {
                return (IdbEngine)EngineType.Assembly.CreateInstance(EngineType.FullName);
            }
            else
            {
                Utils.Logger.Trace(new Exception("undefined database engine!"), true);
                return null;
            }
        }

        public bool TestEngine()
        {
            IdbEngine Engine = CreateEngine();
            if (Engine == null) throw new Exception("create engine failed!");
            using (DbConnection conn = Engine.NewConnection(Setting.ConnectString))
            {
                conn.Open();
                conn.Close();
                return true;
            }
            return false;
        }

        public object Clone()
        {
            DbEngineSetting newsettings = new DbEngineSetting();
            newsettings.Setting = this.Setting;
            return newsettings;
        }

        public override string ToString()
        {
            return Setting.Type.ToString() + "|" + Setting.ConnectString;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element)
            {
                //this.ServerName = reader["ServerName"];
                //this.ParserType = reader["ParserType"];
                if (reader.MoveToContent() != XmlNodeType.Element || reader.LocalName != "Connection") reader.ReadToDescendant("Connection");
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Connection")
                {
                    //this.Connection = (ConnectionSetting)Utils.XmlSerializerObject.ObjFromXml(typeof(ConnectionSetting), reader);
                    //Type EnginType = DbEngineHelper.GetEngineType(reader["Type.Name"]);
                    //IdbEngine tempEngine = (IdbEngine)EnginType.Assembly.CreateInstance(EnginType.FullName);
                    Setting.Type = reader["Type"]; //Convert.ChangeType((string)reader["Type"], typeof(ConnectionType));
                    Setting.ConnectString = reader["ConnectString"];
                }
                reader.Read();
            }

            //ServerAgent = new PlantRequestAgent(Serverinfo);
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            //throw new NotImplementedException();
            //writer.WriteAttributeString("Connection", this.Serverinfo.ConnectInfo.ServerName);
            writer.WriteStartElement("Connection");
            //Utils.XmlSerializerObject.ObjToXml(this.Connection, this.Connection.GetType(), writer);
            writer.WriteAttributeString("Type", Setting.Type);
            writer.WriteAttributeString("ConnectString", Setting.ConnectString);
            writer.WriteEndElement();
        }
    }

    /*
    class DBConnection
    {
        public DbConnection Connection {get; private set;}
        public DBConnection(DbEngineSetting connsettings)
        {
            switch (connsettings.Setting.Type)
            {
                case DbEngineSetting.ConnectionType.ODBC:
                    Connection = new OdbcConnection(connsettings.Setting.ConnectString);
                    break;
                case DbEngineSetting.ConnectionType.SQLCLIENT:
                    Connection = new SqlConnection(connsettings.Setting.ConnectString);
                    break;
            }
        }

        public bool TestConnect()
        {
            try
            {
                Connection.Open();
                Connection.Close();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }

    class DBCommand
    {
        public DbCommand Command { get; private set; }
        private static string[] ParamTypeTags = new string[3] {"$i", "$c", "$n"};

        private DbEngineSetting Settings;
        private DBConnection _connection;
        public DBConnection Connection { 
            get { return _connection;}
            set { _connection = value; SetConnection(); } 
        }
        public DBCommand(DbEngineSetting connsettings)
        {
            Settings = connsettings;
            switch (connsettings.Setting.Type)
            {
                case DbEngineSetting.ConnectionType.ODBC:
                    Command = new OdbcCommand();
                    break;
                case DbEngineSetting.ConnectionType.SQLCLIENT:
                    Command = new SqlCommand();
                    break;
            }
        }

        private void SetConnection()
        {
            Command.Connection = Connection.Connection;
        }

        public DbParameter AddParameter(string name, string value)
        {
            DbParameter p = null;
            int datatype = -1;
            if (value.Length >= 2) Array.IndexOf(ParamTypeTags, value.Substring(0, 2));
            switch (Settings.Setting.Type)
            {
                case DbEngineSetting.ConnectionType.ODBC:
                    p = new OdbcParameter();
                    switch (datatype)
                    {
                        case 0: ((OdbcParameter)p).OdbcType = OdbcType.Int; break;
                        case 1: ((OdbcParameter)p).OdbcType = OdbcType.VarChar; break;
                        case 2: ((OdbcParameter)p).OdbcType = OdbcType.Numeric; break;
                        default: ((OdbcParameter)p).OdbcType = OdbcType.VarChar; break;
                    }                    
                    break;
                case DbEngineSetting.ConnectionType.SQLCLIENT:
                    p = new SqlParameter();
                    switch (datatype)
                    {
                        case 0: ((SqlParameter)p).SqlDbType = SqlDbType.Int; break;
                        case 1: ((SqlParameter)p).SqlDbType = SqlDbType.VarChar; break;
                        case 2: ((SqlParameter)p).SqlDbType = SqlDbType.Float; break;
                        default: ((SqlParameter)p).SqlDbType = SqlDbType.VarChar; break;
                    }                    
                    break;
            }
            p.ParameterName = name;            
            p.Value = value;
            return p;
        }
    }

    class DBDataAdapter
    {
        public DbDataAdapter DataAdapter { get; private set; }
        public DBDataAdapter(DbEngineSetting connsettings, string sql)
        {
            switch (connsettings.Setting.Type)
            {
                case DbEngineSetting.ConnectionType.ODBC:
                    DataAdapter = new OdbcDataAdapter(sql, connsettings.Setting.ConnectString);
                    break;
                case DbEngineSetting.ConnectionType.SQLCLIENT:
                    DataAdapter = new SqlDataAdapter(sql, connsettings.Setting.ConnectString);
                    break;
            }
        }
    }
    */

    public class sqlrunner
    {
        private IdbEngine Engine;
        private DbConnection Connection;
        private DbCommand Command;
        private DbDataAdapter DataAdapter;
        private DbEngineSetting Settings;
        private Stopwatch Timecounter = new Stopwatch();

        public long TimeElapsed = 0;

        public sqlrunner(DbEngineSetting dbsettings)
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

        ~sqlrunner()
        {
            if (Connection != null && (Connection.State == ConnectionState.Open))
            {
                //Connection.Connection.Close();
            }
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
                        DbParameter p = Engine.NewParamenter(param.Key, param.Value);
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
                        DbParameter p = Engine.NewParamenter(param.Key, param.Value);
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
