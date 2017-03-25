using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.ConnectionUI;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.Common;
using System.Data;


namespace sqlstress
{
    public class OdbcEngine : IdbEngine
    {
        private static string[] ParamTypeTags = new string[3] { "$i", "$c", "$n" };
        public OdbcEngine()
        {

        }
        public DbConnection NewConnection(string connstring)
        {
            return new OdbcConnection(connstring);
        }
        public DbCommand NewCommand()
        {
            return new OdbcCommand();
        }
        public DbDataAdapter NewDataAdapter(string connstring, string sql)
        {
            return new OdbcDataAdapter(sql, connstring);
        }
        public DbParameter NewParamenter(string paramanme, string paramvalue)
        {
            DbParameter p = null;
            int datatype = -1;
            if (paramvalue.Length >= 2) Array.IndexOf(ParamTypeTags, paramvalue.Substring(0, 2));

            p = new OdbcParameter();
            switch (datatype)
            {
                case 0: ((OdbcParameter)p).OdbcType = OdbcType.Int; break;
                case 1: ((OdbcParameter)p).OdbcType = OdbcType.VarChar; break;
                case 2: ((OdbcParameter)p).OdbcType = OdbcType.Numeric; break;
                default: ((OdbcParameter)p).OdbcType = OdbcType.VarChar; break;
            }

            p.ParameterName = paramanme;
            p.Value = paramvalue;
            return p;
        }
        public DbEngineInfo GetInfo()
        {
            DbEngineInfo info = new DbEngineInfo();
            info.datasource = Microsoft.Data.ConnectionUI.DataSource.OdbcDataSource;
            info.dataprovider = Microsoft.Data.ConnectionUI.DataProvider.OdbcDataProvider;
            info.Name = "ODBC";
            return info;
        }
    }

    public class SqlClientEngine : IdbEngine
    {
        private static string[] ParamTypeTags = new string[3] { "$i", "$c", "$n" };
        public SqlClientEngine()
        {

        }
        public DbConnection NewConnection(string connstring)
        {
            return new SqlConnection(connstring);
        }
        public DbCommand NewCommand()
        {
            return new SqlCommand();
        }
        public DbDataAdapter NewDataAdapter(string connstring, string sql)
        {
            return new SqlDataAdapter(sql, connstring);
        }
        public DbParameter NewParamenter(string paramanme, string paramvalue)
        {
            DbParameter p = null;
            int datatype = -1;
            if (paramvalue.Length >= 2) Array.IndexOf(ParamTypeTags, paramvalue.Substring(0, 2));

            p = new SqlParameter();
            switch (datatype)
            {
                case 0: ((SqlParameter)p).SqlDbType = SqlDbType.Int; break;
                case 1: ((SqlParameter)p).SqlDbType = SqlDbType.VarChar; break;
                case 2: ((SqlParameter)p).SqlDbType = SqlDbType.Float; break;
                default: ((SqlParameter)p).SqlDbType = SqlDbType.VarChar; break;
            }

            p.ParameterName = paramanme;
            p.Value = paramvalue;
            return p;
        }
        public DbEngineInfo GetInfo()
        {
            DbEngineInfo info = new DbEngineInfo();
            info.datasource = Microsoft.Data.ConnectionUI.DataSource.SqlDataSource;
            info.dataprovider = Microsoft.Data.ConnectionUI.DataProvider.SqlDataProvider;
            info.Name = "SQLCLIENT";
            return info;
        }
    }
    
    public class OleDbEngine : IdbEngine
    {
        private static string[] ParamTypeTags = new string[3] { "$i", "$c", "$n" };
        public OleDbEngine()
        {

        }
        public DbConnection NewConnection(string connstring)
        {
            return new OleDbConnection(connstring);
        }
        public DbCommand NewCommand()
        {
            return new OleDbCommand();
        }
        public DbDataAdapter NewDataAdapter(string connstring, string sql)
        {
            return new OleDbDataAdapter(sql, connstring);
        }
        public DbParameter NewParamenter(string paramanme, string paramvalue)
        {
            DbParameter p = null;
            int datatype = -1;
            if (paramvalue.Length >= 2) Array.IndexOf(ParamTypeTags, paramvalue.Substring(0, 2));

            p = new OleDbParameter();
            switch (datatype)
            {
                case 0: ((OleDbParameter)p).OleDbType = OleDbType.Integer; break;
                case 1: ((OleDbParameter)p).OleDbType = OleDbType.VarChar; break;
                case 2: ((OleDbParameter)p).OleDbType = OleDbType.Double; break;
                default: ((OleDbParameter)p).OleDbType = OleDbType.VarChar; break;
            }

            p.ParameterName = paramanme;
            p.Value = paramvalue;
            return p;
        }
        public DbEngineInfo GetInfo()
        {
            DbEngineInfo info = new DbEngineInfo();
            info.datasource = Microsoft.Data.ConnectionUI.DataSource.AccessDataSource;
            info.dataprovider = Microsoft.Data.ConnectionUI.DataProvider.OleDBDataProvider;
            info.Name = "OLEDB";
            return info;
        }
    }

    public class OracleEngine : IdbEngine
    {
        private static string[] ParamTypeTags = new string[3] { "$i", "$c", "$n" };
        public OracleEngine()
        {

        }
        public DbConnection NewConnection(string connstring)
        {
            return new OracleConnection(connstring);
        }
        public DbCommand NewCommand()
        {
            return new OracleCommand();
        }
        public DbDataAdapter NewDataAdapter(string connstring, string sql)
        {
            return new OracleDataAdapter(sql, connstring);
        }
        public DbParameter NewParamenter(string paramanme, string paramvalue)
        {
            DbParameter p = null;
            int datatype = -1;
            if (paramvalue.Length >= 2) Array.IndexOf(ParamTypeTags, paramvalue.Substring(0, 2));

            p = new OracleParameter();
            switch (datatype)
            {
                case 0: ((OracleParameter)p).OracleType = OracleType.Int32; break;
                case 1: ((OracleParameter)p).OracleType = OracleType.VarChar; break;
                case 2: ((OracleParameter)p).OracleType = OracleType.Double; break;
                default: ((OracleParameter)p).OracleType = OracleType.VarChar; break;
            }

            p.ParameterName = paramanme;
            p.Value = paramvalue;
            return p;
        }
        public DbEngineInfo GetInfo()
        {
            DbEngineInfo info = new DbEngineInfo();
            info.datasource = Microsoft.Data.ConnectionUI.DataSource.OracleDataSource;
            info.dataprovider = Microsoft.Data.ConnectionUI.DataProvider.OracleDataProvider;
            info.Name = "ORACLE";
            return info;
        }
    }
}
