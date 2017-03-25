using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.ConnectionUI;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.Common;
using System.Data;

namespace sqlstress
{
    public struct DbEngineInfo
    {
        public string Name;
        public Microsoft.Data.ConnectionUI.DataSource datasource;
        public Microsoft.Data.ConnectionUI.DataProvider dataprovider;
    }
    public interface IdbEngine
    {
        DbConnection NewConnection(string connstring);
        DbCommand NewCommand();
        DbDataAdapter NewDataAdapter(string connstring, string sql);
        DbParameter NewParamenter(string paramanme, string paramvalue);
        DbEngineInfo GetInfo();
    }

}
