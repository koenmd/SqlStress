using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using System.Drawing.Design;
using System.Data;

namespace sqlstress
{
    /// <summary>
    /// SQL语句描述，支持参数
    /// </summary>
    public class sqlstatement
    {
        public string sql { get; set; }
        public Dictionary<string, string> sqlparams { get; set; }
        public static sqlstatement Empty = new sqlstatement("", null);
        public sqlstatement(string _sql, Dictionary<string, string> _sqlparams)
        {
            sql = _sql;
            sqlparams = _sqlparams;
        }
    }

    [Serializable]
    [TypeConverter(typeof(XMLConvertor<sqlserials>))]
    public class sqlserials : IXmlSerializable
    {
        public bool Paramexecute { get; set; } = false;
        public sqlserials()
        {
            SqlText = "";
            ParamText = "";
        }
        public sqlserials(string _sqltext = "", string _paramtext = "")
        {
            SqlText = _sqltext;
            ParamText = _paramtext;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(sqlstringeditor), typeof(UITypeEditor))]
        [Localizable(true)]
        public string SqlText { get; set; } //private string _sqltext = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(sqlparamsditor), typeof(UITypeEditor))]
        [Localizable(true)]
        public string ParamText { get; set; } //private string _paramtext = "";

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element)
            {
                if (reader.MoveToContent() != XmlNodeType.Element || reader.LocalName != "sqlserials") reader.ReadToDescendant("sqlserials");
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "sqlserials")
                {
                    this.SqlText = reader.ReadElementString("sql");
                    this.ParamText = reader.ReadElementString("params");
                    this.Paramexecute = bool.Parse(reader.ReadElementString("parameterlized"));
                }
                reader.Read();
            }
            //ServerAgent = new PlantRequestAgent(Serverinfo);
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("sqlserials");
            writer.WriteElementString("sql", this.SqlText);
            writer.WriteElementString("params", this.ParamText);
            writer.WriteElementString("parameterlized", this.Paramexecute.ToString());
            writer.WriteEndElement();
        }

        public override string ToString()
        {
            return string.Format("sql({0}|params({1}))", SqlText.Length, ParamText.Length);
        }
    }

    /// <summary>
    /// SQL表达式，生成批量SQL语句，支持参数，可以持久化
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(XMLConvertor<sqlexpression>))]
    public class sqlexpression : sqlserials, IEnumerable
    {   
        //public static string DIV_SQLSTRING = "\r\ngo\r\n";
        public static string DIV_PARSTRING = "\r\n";
        public static string DIV_PARITEMS = "\t";

        public static sqlexpression Empty = new sqlexpression();

        private sqlparamvalueTranslater Translater = new sqlparamvalueTranslater();
        /*
        [Browsable(false)]
        internal string[] Sqls
        {
            get
            {
                return SqlText.Split(new string[] { DIV_SQLSTRING }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        */
        [Browsable(false)]
        internal string[] ParamsValue
        {
            get
            {
                return ParamText.Split(new string[] { DIV_PARSTRING }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        [Browsable(false)]
        public List<string> ParamNames
        {
            get
            {
                List<KeyValuePair<int, string>> paraminfos = getSQLParams(SqlText);
                List<string> paramnames = new List<string>();
                foreach (KeyValuePair<int, string> kv in paraminfos)
                {
                    if (!paramnames.Contains(kv.Value))
                    {
                        paramnames.Add(kv.Value);
                    }
                }
                return paramnames;
            }
        }

        [Browsable(false)]
        public DataTable ParamsData
        {
            get
            {
                DataTable ParamsTable = new DataTable();

                ParamsTable.BeginInit();
                foreach (string paramname in ParamNames)
                {
                    DataColumn dc = ParamsTable.Columns.Add(paramname);
                }

                List<string> paramnames = this.ParamNames;
                foreach (string paramstr in this.ParamsValue)
                {
                    DataRow row = ParamsTable.NewRow();
                    string[] paramvalues = paramstr.Split(DIV_PARITEMS.ToArray());
                    for (int i = 0; i < paramnames.Count; i++)
                    {
                        row[paramnames[i]] = paramvalues[i];
                    }
                    ParamsTable.Rows.Add(row);
                }
                ParamsTable.AcceptChanges();
                ParamsTable.EndInit();

                return ParamsTable;
            }
            set
            {
                if (value == null) return;
                List<string> ParamLines = new List<string>();
                List<string> Linevalues = new List<string>();
                foreach (DataRow row in value.Rows)
                {
                    Linevalues.Clear();
                    for (int i = 0; i < value.Columns.Count; i++)
                    {
                        Linevalues.Add(row[i].ToString());
                    }
                    ParamLines.Add(string.Join(DIV_PARITEMS, Linevalues));
                }
                this.ParamText = string.Join(DIV_PARSTRING, ParamLines);
            }
        }

        public static List<KeyValuePair<int, string>> getSQLParams(string sql)
        {
            //Dictionary<string, List<int>> ps = new Dictionary<string, List<int>>();
            List<KeyValuePair<int, string>> ps = new List<KeyValuePair<int, string>>();
            //Regex r = new Regex(@"(@\w+\b|\?\w*)");
            Regex r = new Regex(@"(@\w+\b)");
            var matches = r.Matches(sql);
            foreach (Match item in matches)
            {
                /*
                if (!ps.Keys.Contains(item.Value))
                {
                    ps.Add(item.Value == "?" ? ps.Count.ToString() : item.Value, new List<int> { item.Index });
                }
                else
                {
                    ps[item.Value].Add(item.Index);
                }
                */
                ps.Add(new KeyValuePair<int, string>(item.Index, item.Value));
            }
            return ps;
        }

        public IEnumerator GetEnumerator()
        {
            if (Paramexecute && this.ParamNames.Count > 0)
            {
                List<string> paramnames = this.ParamNames;
                foreach (string paramstr in this.ParamsValue)
                {
                    string[] paramvalues = paramstr.Split(DIV_PARITEMS.ToArray());
                    Dictionary<string, string> sqlparams = new Dictionary<string, string>();
                    for (int i = 0; i < paramnames.Count; i++)
                    {
                        sqlparams.Add(paramnames[i], paramvalues.Length > i ? paramvalues[i] : string.Empty);
                    }
                    yield return new sqlstatement(this.SqlText, sqlparams);
                }
            }
            else
            {        
                if (this.ParamsValue.Length <= 0)
                {
                    for (int i = 0; i == 0; i++)
                    {
                        yield return new sqlstatement(SqlText, null);
                    }
                }
                            
                List<KeyValuePair<int, string>> paramslist = sqlexpression.getSQLParams(SqlText);
                //paramslist.Sort((x, y) => { return x.Key <= y.Key ? 1 : 0; });

                foreach (string paramstr in this.ParamsValue)
                {
                    string newsql = "";
                    string[] paramvalues = paramstr.Split(DIV_PARITEMS.ToArray());
                    Dictionary<string, string> sqlparams = new Dictionary<string, string>();
                    for (int i = 0; i < ParamNames.Count; i++)
                    {
                        sqlparams.Add(ParamNames[i], paramvalues.Length > i ? paramvalues[i] : string.Empty);
                    }

                    int currentpos = 0;
                    foreach (KeyValuePair<int, string> paraminfo in paramslist)
                    {
                        newsql += SqlText.Substring(currentpos, paraminfo.Key - currentpos);
                        newsql += Translater.GetValueString(paraminfo.Value, sqlparams[paraminfo.Value]);
                        currentpos = paraminfo.Key + paraminfo.Value.Length;
                    }
                    newsql += SqlText.Substring(currentpos, SqlText.Length - currentpos);

                    yield return new sqlstatement(newsql, null);
                }
            }
        }
    }

    /// <summary>
    /// 简化版的SQL表达式，只能生成一条单一的SQL语句，不支持参数
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(XMLConvertor<sqlexpsimple>))]
    public class sqlexpsimple : sqlserials, IEnumerable
    {
        [Browsable(false)]
        public string ParamText { get; set; } //private string _paramtext = "";
        [Browsable(false)]
        public bool Paramexecute { get; set; }
        public static sqlexpsimple Empty = new sqlexpsimple();

        public static string DIV_SQLSTRING = "\r\ngo\r\n";
        [Browsable(false)]
        internal string[] Sqls
        {
            get
            {
                return SqlText.Split(new string[] { DIV_SQLSTRING }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        public IEnumerator GetEnumerator()
        {
            foreach (string sql in this.Sqls)
            {
                yield return new sqlstatement(sql, null);
            }
        }

        public override string ToString()
        {
            return SqlText.Length > 0 ? string.Format("sql({0})", SqlText.Length) : "";
        }
    }

    public class sqlparamvalueTranslater : DbParamTranslater
    {
        private static string[] ParamTypeTags = new string[3] { "$i_", "$c_", "$n_" };
        public override DbParamType Translate(string paramname, string paramvalue)
        {
            DbParamType result = DbParamType.VARCHAR;
            int datatype = -1;
            if (paramvalue.Length >= 3)
            {
                datatype = Array.IndexOf(ParamTypeTags, paramvalue.Substring(0, 3));
                switch (datatype)
                {
                    case 0:
                        result = DbParamType.INT;
                        break;
                    case 1:
                        result = DbParamType.VARCHAR;
                        break;
                    case 2:
                        result = DbParamType.FLOAT;
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
        public string GetValueString(string paramname, string paramvalue)
        {
            switch (Translate(paramname, paramvalue))
            {
                case DbParamType.INT: return paramvalue;
                case DbParamType.VARCHAR: return "'" + paramvalue + "'"; 
                case DbParamType.FLOAT: return paramvalue;
                default: return "'" + paramvalue + "'"; 
            }
        }
    }
}
