using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data.Common;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Data;
using System.Drawing;

namespace sqlstress
{ 
    [Serializable]
    public class StressScheme: IXmlSerializable
    {
        public enum SchemeOption_RunMode
        {
            InOrder = 0,
            Random = 1,
        }

        [CategoryAttribute(Resource.CAT_BASE), DisplayName(Resource.CAT_BASE_NAME), Browsable(true), ReadOnly(true)]
        public string SchemeName { get; set; }
        [CategoryAttribute(Resource.CAT_EXEC), DisplayName(Resource.CAT_EXEC_THREAD), Browsable(true)]
        public int run_threads { get; set; } = 100;
        [CategoryAttribute(Resource.CAT_EXEC), DisplayName(Resource.CAT_EXEC_MAXTIMES), Browsable(true)]
        public int run_maxtimes { get; set; } = 1000000;
        [CategoryAttribute(Resource.CAT_EXEC), DisplayName(Resource.CAT_EXEC_WAY), Browsable(true)]
        public SchemeOption_RunMode run_mode { get; set; } = SchemeOption_RunMode.InOrder;
        //public bool run_withparams = false;
        [CategoryAttribute(Resource.CAT_EXEC), DisplayName(Resource.CAT_EXEC_GETRESULT), Browsable(true)]
        public bool run_withresult { get; set; } = false;
        //[NonSerialized]
        [CategoryAttribute(Resource.CAT_TASK), DisplayName(Resource.CAT_TASK_INIT), Browsable(true)]
        [Description("sql execute at start")]
        public sqlexpsimple sql_init { get; set; } = new sqlexpsimple();
        //[NonSerialized]
        [CategoryAttribute(Resource.CAT_TASK), DisplayName(Resource.CAT_TASK_FINIT), Browsable(true)]
        [Description("sql execute at stop")]
        public sqlexpsimple sql_finit { get; set; } = new sqlexpsimple();
        //[NonSerialized]
        [CategoryAttribute(Resource.CAT_TASK), DisplayName(Resource.CAT_TASK_STRESSINIT), Browsable(true)]
        [Description("sql statement")]
        public sqlexpsimple sql_stressfirst { get; set; } = new sqlexpsimple();
        //[NonSerialized]
        [CategoryAttribute(Resource.CAT_TASK), DisplayName(Resource.CAT_TASK_STRESS), Browsable(true)]
        [Description(Resource.CAT_TASK_STRESSDETAIL)]
        public sqlexpression sql_stress { get; set; } = new sqlexpression();
        //[NonSerialized]
        [CategoryAttribute(Resource.CAT_CONN), DisplayName(Resource.CAT_CONN_SETT), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(dbconneditor), typeof(UITypeEditor))]
        [Localizable(true)]
        [Description("db connection wizard")]
        public DbEngineSetting dbsettings { get; set; } = new DbEngineSetting();
        //[NonSerialized]
        [CategoryAttribute(Resource.CAT_CONN), DisplayName(Resource.CAT_CONN_USUAL), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(dbusualconneditor), typeof(UITypeEditor))]
        [Localizable(true)]
        [Description("quick set connection")]
        public EnginSettingsHelper dbsettingshelper { get; set; } = EnginSettingsHelper.LoadEnginSettingsHelper();

        [CategoryAttribute(Resource.CAT_EXEC), DisplayName(Resource.CAT_EXEC_COLOR), Browsable(true)]
        [Description("paint color")]
        public Color PaintColor { get { return _paintcolor; } set { _paintcolor = value; NotifyPropertyChanged("PaintColor");  }} private Color _paintcolor = Color.LightGreen;

        private SchemeReaderWriter ReaderWriter;

        public static string DIV_SQLSTRING = "go\r\n";
        public static string DIV_PARSTRING = "\r\n";
        public static string DIV_PARITEMS = "\t";

        [Browsable(false)]
        public int run_feed = 0;

        [Browsable(false)]
        public string lastparamsql = "";

        [Browsable(false)]
        public string StressqlStr
        {
            get
            {
                /*
                string result = "";
                foreach(string s in stress_sqls)
                {
                    result += s + ";\r\n";
                }
                 * */
                //return string.Join(DIV_SQLSTRING, sql_stress);
                return sql_stress.SqlText;
            }
            set
            {
                //sql_stress = value.Split(new string[] { DIV_SQLSTRING }, StringSplitOptions.RemoveEmptyEntries);
                sql_stress.SqlText = value;
            }
        }

        [Browsable(false)]
        public string ParamssqlStr
        {
            get
            {
                /*
                string result = "";
                foreach (string s in stress_params)
                {
                    result += s + "\r\n";
                }
                 * */
                //return string.Join(DIV_PARSTRING, stress_params);
                return sql_stress.ParamText;
            }
            set
            {
                //stress_params = value.Split(new string[] { DIV_PARSTRING }, StringSplitOptions.RemoveEmptyEntries);
                sql_stress.ParamText = value;
            }
        }

        public StressScheme()
        {
            ReaderWriter = new SchemeReaderWriter(this);
            //sql_stress = new string[1];
            //stress_params = new string[1];
            dbsettingshelper.Scheme = this;
        }

        public StressScheme(string _schemename) : this()
        {
            this.SchemeName = _schemename;
            //ReaderWriter = new SchemeReaderWriter(this);
            //sql_stress = new string[1];
            //stress_params = new string[1];
            ReaderWriter.LoadScheme();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string ProName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(ProName));
            }
        }

        public void CopyTo(StressScheme newScheme)
        {
            newScheme.SchemeName = this.SchemeName;
            newScheme.run_threads = this.run_threads;
            newScheme.run_maxtimes = this.run_maxtimes;
            newScheme.run_mode = this.run_mode;
            //newScheme.run_withparams = this.run_withparams;
            newScheme.run_withresult = this.run_withresult;
            newScheme.sql_init = this.sql_init;
            newScheme.sql_finit = this.sql_finit;
            newScheme.sql_stress = this.sql_stress;
            newScheme.sql_stressfirst = this.sql_stressfirst;
            //newScheme.stress_params = this.stress_params;
            newScheme.dbsettings = this.dbsettings;
            newScheme.PaintColor = this.PaintColor;
            newScheme.lastparamsql = this.lastparamsql;
        }

        public void Load()
        {
            ReaderWriter.LoadScheme();
        }
        public void Save()
        {
            ReaderWriter.SaveScheme();
        }

        public void Delete()
        {
            ReaderWriter.DeleteScheme();
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element)
            {
                this.SchemeName = reader["scheme"];
                this.run_threads = int.Parse(reader["run_threads"]);
                this.run_maxtimes = int.Parse(reader["run_maxtimes"]);
                this.run_mode = (SchemeOption_RunMode)int.Parse(reader["run_mode"]);
                //this.run_withparams = bool.Parse(reader["run_withparams"]);
                this.run_withresult = bool.Parse(reader["run_withresult"]);
                try
                {                    
                    this.PaintColor = Color.FromName(reader["paintcolor"]);
                    this.sql_stress.Paramexecute = bool.Parse(reader["parameterlized"]);
                }
                catch (Exception) {}                
                if (reader.MoveToContent() != XmlNodeType.Element || reader.LocalName != "dbsettings") reader.ReadToDescendant("dbsettings");
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "dbsettings")
                {
                    this.dbsettings = (DbEngineSetting)Utils.XmlSerializerObject.ObjFromXml(typeof(DbEngineSetting), reader);
                }
                if (reader.MoveToContent() != XmlNodeType.Element || reader.LocalName != "others") reader.ReadToDescendant("others");
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "others")
                {
                    this.lastparamsql = reader["lastparamsql"];
                }
                reader.Read();
            }
            //ServerAgent = new PlantRequestAgent(Serverinfo);
            //ReaderWriter.LoadScheme();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            //throw new NotImplementedException();
            writer.WriteAttributeString("scheme", this.SchemeName);
            writer.WriteAttributeString("run_threads", this.run_threads.ToString());
            writer.WriteAttributeString("run_maxtimes", this.run_maxtimes.ToString());
            writer.WriteAttributeString("run_mode", ((int)(this.run_mode)).ToString());
            //writer.WriteAttributeString("run_withparams", this.run_withparams.ToString());
            writer.WriteAttributeString("run_withresult", this.run_withresult.ToString());
            writer.WriteAttributeString("parameterlized", this.sql_stress.Paramexecute.ToString());
            writer.WriteAttributeString("paintcolor", this.PaintColor.Name);
            //writer.WriteStartElement("sql_init");
            //writer.WriteElementString("", this.sql_init);
            //writer.WriteEndElement();
            writer.WriteStartElement("dbsettings");
            Utils.XmlSerializerObject.ObjToXml(this.dbsettings, this.dbsettings.GetType(), writer);
            writer.WriteEndElement();
            writer.WriteStartElement("others");
            writer.WriteAttributeString("lastparamsql", lastparamsql); 
            writer.WriteEndElement();

            //ReaderWriter.SaveScheme();
        }

        class SchemeReaderWriter
        {
            private const string filename_scheme = "case.xml";
            private const string filename_stress = "stress.sql";
            private const string filename_stressinit = "stressfirst.sql";
            private const string filename_params = "params.sql";
            private const string filename_init = "init.sql";
            private const string filename_finit = "finit.sql";

            private StressScheme Scheme = null;
            private string SchemePath
            {
                get
                {
                    return string.Format("{0}\\{1}\\", Resource.SCHEMEPATH, Scheme.SchemeName);
                }
            }

            //private string SchemePath;
            public SchemeReaderWriter(StressScheme _Scheme)
            {
                Scheme = _Scheme;
                //SchemePath = string.Format("{0}\\{1}\\", Resource.SCHEMEPATH, _Scheme.scheme);
            }

            public void LoadScheme()
            {
                if (!Directory.Exists(SchemePath))
                {
                    return;
                }

                if (File.Exists(SchemePath + filename_scheme))
                {
                    StressScheme NewScheme = Utils.XmlSerializerObject.ObjFromXmlFile<StressScheme>(SchemePath + filename_scheme);
                    NewScheme.CopyTo(Scheme);
                }
                if (File.Exists(SchemePath + filename_stress))
                {
                    //string stresssqls = Utils.ToolBox.FileToString(SchemePath + filename_stress);
                    //Scheme.stress_sqls = stresssqls.Split(new string[] { ";\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    Scheme.StressqlStr = Utils.ToolBox.FileToString(SchemePath + filename_stress);
                }
                if (File.Exists(SchemePath + filename_params))
                {
                    //string stressparams = Utils.ToolBox.FileToString(SchemePath + filename_params);
                    //Scheme.stress_params = stressparams.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    Scheme.ParamssqlStr = Utils.ToolBox.FileToString(SchemePath + filename_params);
                }
                if (File.Exists(SchemePath + filename_init))
                {
                    Scheme.sql_init.SqlText = Utils.ToolBox.FileToString(SchemePath + filename_init);
                }
                if (File.Exists(SchemePath + filename_finit))
                {
                    Scheme.sql_finit.SqlText = Utils.ToolBox.FileToString(SchemePath + filename_finit);
                }
                if (File.Exists(SchemePath + filename_stressinit))
                {
                    Scheme.sql_stressfirst.SqlText = Utils.ToolBox.FileToString(SchemePath + filename_stressinit);
                }
            }

            public void SaveScheme()
            {
                if (!Directory.Exists(SchemePath))
                {
                    Directory.CreateDirectory(SchemePath);
                }

                /*
                using (FileStream fs = new FileStream(SchemePath + filename_stress, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (string sql in Scheme.stress_sqls)
                        {
                            sw.WriteLine(sql + ";");
                        }
                        sw.Flush();
                    }
                }
                using (FileStream fs = new FileStream(SchemePath + filename_params, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (string param in Scheme.stress_params)
                        {
                            sw.WriteLine(param);
                        }
                        sw.Flush();
                    }
                }
                 * */
                Utils.ToolBox.StringToFile(SchemePath + filename_stress, Scheme.StressqlStr);
                Utils.ToolBox.StringToFile(SchemePath + filename_params, Scheme.ParamssqlStr);
                Utils.ToolBox.StringToFile(SchemePath + filename_init, Scheme.sql_init.SqlText);
                Utils.ToolBox.StringToFile(SchemePath + filename_finit, Scheme.sql_finit.SqlText);
                Utils.ToolBox.StringToFile(SchemePath + filename_stressinit, Scheme.sql_stressfirst.SqlText);
                Utils.XmlSerializerObject.ObjToXMLFile(SchemePath + filename_scheme, Scheme, typeof(StressScheme), null, "");
            }

            public void DeleteScheme()
            {
                Directory.Delete(SchemePath, true);
            }
        }
    }

    class SchemeFeeder : IDbStressFeeder
    {
        /*
        public struct SqlTask
        {
            public string sql;
            public string paramvalues;
            public string[] parameters;
        }
        */

        private StressScheme Scheme;
        private List<sqlstatement> FeedTasks = new List<sqlstatement>();

        private Queue<int>[] ThreadTasks;

        private object syncobj = new object();
        //private char[] paramvaluedivide = StressScheme.DIV_PARITEMS.ToArray();
        public SchemeFeeder(StressScheme _Scheme)
        {
            Scheme = _Scheme;
        }

        public void FeederInit(DbStressEngine engin)
        {
            //进度重置
            Scheme.run_feed = 0;

            //形参转换为
            List<KeyValuePair<int, string>> paramsinfo = sqlexpression.getSQLParams(Scheme.sql_stress.SqlText);
            IDbEngine sqlengin = engin.Settings.CreateEngine();

            foreach (sqlstatement st in Scheme.sql_stress)
            {
                if (Scheme.sql_stress.Paramexecute && paramsinfo.Count > 0)
                {
                    string newsql = "";
                    int currentpos = 0;
                    foreach (KeyValuePair<int, string> paraminfo in paramsinfo)
                    {
                        newsql += st.sql.Substring(currentpos, paraminfo.Key - currentpos);
                        newsql += sqlengin.ParameterPattern(paraminfo.Value.Substring(1));
                        currentpos = paraminfo.Key + paraminfo.Value.Length;
                    }
                    newsql += st.sql.Substring(currentpos, st.sql.Length - currentpos);
                    st.sql = newsql;
                }
                FeedTasks.Add(st);
            }

            ThreadTasks = new Queue<int>[Scheme.run_threads];
            for (int i = 0; i < Scheme.run_threads; i++)
            {
                ThreadTasks[i] = new Queue<int>();
            }

            int taskremain = Scheme.run_maxtimes;
            if (Scheme.run_mode == StressScheme.SchemeOption_RunMode.Random)
            {
                Random r = new Random(DateTime.Now.Millisecond);
                while (taskremain-- > 0)
                {
                    ThreadTasks[taskremain % Scheme.run_threads].Enqueue(r.Next(0, FeedTasks.Count));
                }
            }
            else
            {
                while (taskremain-- > 0)
                {
                    ThreadTasks[taskremain % Scheme.run_threads].Enqueue((Scheme.run_maxtimes - taskremain - 1) % FeedTasks.Count);
                }
            }
        }

        public bool FeedNext(int threadindex, ref string sql, ref Dictionary<string, string> parameters)
        {
            if (threadindex < 0 || threadindex >= ThreadTasks.Length) return false;
            int ntask = -1;

            try
            {                
                if (ThreadTasks[threadindex].Count > 0)
                {
                    ntask = ThreadTasks[threadindex].Dequeue();
                }
                else
                {
                    Queue<int> Tasksn = ThreadTasks.First(p => p.Count > 0);
                    if (Tasksn != null)
                    {
                        ntask = Tasksn.Dequeue();
                    }                        
                }                
            }
            catch (Exception)
            {                
                return false;
            }

            if (ntask < 0) return false;

            sql = FeedTasks[ntask].sql;
            parameters = FeedTasks[ntask].sqlparams;
            Scheme.run_feed++;
            return true;
        }

        public string[] ExecInit()
        {
            /*
            sqlrunner sqlr = new sqlrunner(Scheme.dbsettings);
            if (Scheme.sql_init != "")
            {
                sqlr.Run_NoResult(Scheme.sql_init, null, false);
            }          
             * */
            //return Scheme.sql_init.Split(new string[] { StressScheme.DIV_SQLSTRING }, StringSplitOptions.RemoveEmptyEntries);
            return Scheme.sql_init.Sqls;
        }

        public string[] ExecFinished()
        {
            return Scheme.sql_finit.Sqls;
        }

        public string[] FeedFirst()
        {
            return Scheme.sql_stressfirst.Sqls;
        }
    }
}
