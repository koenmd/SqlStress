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
    public enum DbParamType
    {
        INT = 0,
        VARCHAR = 1,
        FLOAT = 2,
    }

    public class DbParamTranslater
    {
        public static DbParamTranslater DefaultTranslater = new DbParamTranslater();
        public virtual DbParamType Translate(string paramname, string paramvalue)
        {
            return DbParamType.VARCHAR;
        }
    }

    public class OdbcEngine : IDbEngine
    {
        //private static string[] ParamTypeTags = new string[3] { "$i_", "$c_", "$n_" };
        public OdbcEngine()
        {

        }
        public DbConnection NewConnection(string connstring, int timeout = 0)
        {
            var result = new OdbcConnection(connstring);
            if (timeout > 0) result.ConnectionTimeout = timeout;
            return result;
        }
        public DbCommand NewCommand()
        {
            return new OdbcCommand();
        }
        public DbDataAdapter NewDataAdapter(string connstring, string sql)
        {
            return new OdbcDataAdapter(sql, connstring);
        }
        public DbParameter NewParameter(string paramanme, string paramvalue, DbParamType paramtype = DbParamType.VARCHAR)
        {
            DbParameter p = null;
            //int datatype = -1;
            //if (paramvalue.Length >= 3) Array.IndexOf(ParamTypeTags, paramvalue.Substring(0, 3));

            p = new OdbcParameter();
            switch (paramtype)
            {
                case DbParamType.INT: ((OdbcParameter)p).OdbcType = OdbcType.Int; break;
                case DbParamType.VARCHAR: ((OdbcParameter)p).OdbcType = OdbcType.VarChar; break;
                case DbParamType.FLOAT: ((OdbcParameter)p).OdbcType = OdbcType.Numeric; break;
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
        public string ParameterPattern(string paramname)
        {
            return "?";
        }
    }

    public class SqlClientEngine : IDbEngine
    {
        //private static string[] ParamTypeTags = new string[3] { "$i_", "$c_", "$n_" };
        public SqlClientEngine()
        {

        }
        public DbConnection NewConnection(string connstring, int timeout = 0)
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
        public DbParameter NewParameter(string paramanme, string paramvalue, DbParamType paramtype = DbParamType.VARCHAR)
        {
            DbParameter p = null;
            //int datatype = -1;
            //if (paramvalue.Length >= 3) Array.IndexOf(ParamTypeTags, paramvalue.Substring(0, 3));

            p = new SqlParameter();
            switch (paramtype)
            {
                case DbParamType.INT: ((SqlParameter)p).SqlDbType = SqlDbType.Int; break;
                case DbParamType.VARCHAR: ((SqlParameter)p).SqlDbType = SqlDbType.VarChar; break;
                case DbParamType.FLOAT: ((SqlParameter)p).SqlDbType = SqlDbType.Float; break;
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
        public string ParameterPattern(string paramname)
        {
            return "@" + paramname;
        }
    }
    
    public class OleDbEngine : IDbEngine
    {
        //private static string[] ParamTypeTags = new string[3] { "$i_", "$c_", "$n_" };
        public OleDbEngine()
        {

        }
        public DbConnection NewConnection(string connstring, int timeout = 0)
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
        public DbParameter NewParameter(string paramanme, string paramvalue, DbParamType paramtype = DbParamType.VARCHAR)
        {
            DbParameter p = null;
            //int datatype = -1;
            //if (paramvalue.Length >= 3) Array.IndexOf(ParamTypeTags, paramvalue.Substring(0, 3));

            p = new OleDbParameter();
            switch (paramtype)
            {
                case DbParamType.INT: ((OleDbParameter)p).OleDbType = OleDbType.Integer; break;
                case DbParamType.VARCHAR: ((OleDbParameter)p).OleDbType = OleDbType.VarChar; break;
                case DbParamType.FLOAT: ((OleDbParameter)p).OleDbType = OleDbType.Double; break;
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
        public string ParameterPattern(string paramname)
        {
            return "?";
        }
    }

    public class OracleEngine : IDbEngine
    {
        //private static string[] ParamTypeTags = new string[3] { "$i_", "$c_", "$n_" };
        public OracleEngine()
        {

        }
        public DbConnection NewConnection(string connstring, int timeout = 0)
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
        public DbParameter NewParameter(string paramanme, string paramvalue, DbParamType paramtype = DbParamType.VARCHAR)
        {
            DbParameter p = null;
            //int datatype = -1;
            //if (paramvalue.Length >= 3) Array.IndexOf(ParamTypeTags, paramvalue.Substring(0, 3));

            p = new OracleParameter();
            switch (paramtype)
            {
                case DbParamType.INT: ((OracleParameter)p).OracleType = OracleType.Int32; break;
                case DbParamType.VARCHAR: ((OracleParameter)p).OracleType = OracleType.VarChar; break;
                case DbParamType.FLOAT: ((OracleParameter)p).OracleType = OracleType.Double; break;
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
        public string ParameterPattern(string paramname)
        {
            return "?";
        }
    }
}
