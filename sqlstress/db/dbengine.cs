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
    public interface IDbEngine
    {
        DbConnection NewConnection(string connstring, int timeout = 0);
        DbCommand NewCommand();
        DbDataAdapter NewDataAdapter(string connstring, string sql);
        DbParameter NewParameter(string paramanme, string paramvalue, DbParamType paramtype = DbParamType.VARCHAR);
        DbEngineInfo GetInfo();
        string ParameterPattern(string paramname);
    }

}
