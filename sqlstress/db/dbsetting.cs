using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Xml;
using System.Xml.Serialization;
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
                    if (typeof(IDbEngine).IsAssignableFrom(t) && (t != typeof(IDbEngine)))
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
                IDbEngine tempEngine = (IDbEngine)t.Assembly.CreateInstance(t.FullName);
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
                IDbEngine tempEngine = (IDbEngine)t.Assembly.CreateInstance(t.FullName);
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

        public IDbEngine CreateEngine()
        {
            Type EngineType = DbEngineHelper.GetEngineType(Setting.Type);
            if (EngineType != null)
            {
                return (IDbEngine)EngineType.Assembly.CreateInstance(EngineType.FullName);
            }
            else
            {
                Utils.Logger.Trace(new Exception("undefined database engine!"), true);
                return null;
            }
        }

        public bool TestEngine(ref string message)
        {
            IDbEngine testEngine = CreateEngine();
            if (testEngine == null) throw new Exception("create engine failed!");
            using (DbConnection conn = testEngine.NewConnection(Setting.ConnectString, 10))
            {
                try
                {                    
                    conn.Open();
                    conn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    message = e.Message + e.StackTrace;
                }
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
}
