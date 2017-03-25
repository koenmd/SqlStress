using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.ConnectionUI;
using System.Windows.Forms;
using System.Reflection;

namespace sqlstress
{
    public class dbConnectonwiz
    {
        public static void WizSetConnection(ref DbEngineSetting.EngineSetting Setting)
        {
            DbEngineSetting.EngineSetting NewSetting = new DbEngineSetting.EngineSetting();
            //string conn = Setting.ConnectString;
            Microsoft.Data.ConnectionUI.DataConnectionDialog connDialog = new Microsoft.Data.ConnectionUI.DataConnectionDialog();

            Type[] EngineTypes = DbEngineHelper.GetEngineTypes();
            DbEngineInfo SelectEngine = new DbEngineInfo();
            foreach (Type t in EngineTypes)
            {
                IdbEngine tempEngine = (IdbEngine)t.Assembly.CreateInstance(t.FullName);
                DbEngineInfo info = tempEngine.GetInfo();
                connDialog.DataSources.Add(info.datasource);
                if (Setting.Type == info.Name)
                {
                    SelectEngine = info;
                }
            }
            //Microsoft.Data.ConnectionUI.DataSource.AddStandardDataSources(connDialog);
            try
            {
                connDialog.SelectedDataSource = SelectEngine.datasource;
                connDialog.SelectedDataProvider = SelectEngine.dataprovider;
                connDialog.ConnectionString = Setting.ConnectString;
            }
            catch (Exception ex)
            {
                //donothing;
            }

            if (Microsoft.Data.ConnectionUI.DataConnectionDialog.Show(connDialog) == DialogResult.OK)
            {
                //NewSetting.Type.datasource = connDialog.SelectedDataSource;
                //NewSetting.Type.dataprovider = connDialog.SelectedDataProvider;
                Type EnginType = DbEngineHelper.GetEngineType(connDialog.SelectedDataSource, connDialog.SelectedDataProvider);
                if (EnginType != null)
                {
                    IdbEngine tempEngine = (IdbEngine)EnginType.Assembly.CreateInstance(EnginType.FullName);
                    DbEngineInfo info = tempEngine.GetInfo();
                    NewSetting.Type = info.Name;
                    NewSetting.ConnectString = connDialog.ConnectionString;
                    Setting = NewSetting;
                }
                else
                {
                    Utils.Logger.Trace(new Exception("undefined database engine"), true);
                }
            }

            /*
            //添加数据源列表，可以向窗口中添加自己程序所需要的数据源类型 必须增加以下几项中任一一项
            //connDialog.DataSources.Add(Microsoft.Data.ConnectionUI.DataSource.AccessDataSource); // Access 
            connDialog.DataSources.Add(Microsoft.Data.ConnectionUI.DataSource.OdbcDataSource); // ODBC
            //connDialog.DataSources.Add(Microsoft.Data.ConnectionUI.DataSource.OracleDataSource); // Oracle 
            connDialog.DataSources.Add(Microsoft.Data.ConnectionUI.DataSource.SqlDataSource); // Sql Server
            //connDialog.DataSources.Add(Microsoft.Data.ConnectionUI.DataSource.SqlFileDataSource); // Sql Server File

            // 初始化
            if (Setting.Type == DbEngineSetting.ConnectionType.SQLCLIENT)
            {
                connDialog.SelectedDataSource = Microsoft.Data.ConnectionUI.DataSource.SqlDataSource;
                connDialog.SelectedDataProvider = Microsoft.Data.ConnectionUI.DataProvider.SqlDataProvider;
                try
                {
                    connDialog.ConnectionString = Setting.ConnectString;
                }
                catch (Exception)
                {
                    //donothing;
                }
            }
            else if (Setting.Type == DbEngineSetting.ConnectionType.ODBC)
            {
                connDialog.SelectedDataSource = Microsoft.Data.ConnectionUI.DataSource.OdbcDataSource;
                connDialog.SelectedDataProvider = Microsoft.Data.ConnectionUI.DataProvider.OdbcDataProvider;
                connDialog.ConnectionString = Setting.ConnectString;
            }


            //只能够通过DataConnectionDialog类的静态方法Show出对话框
            //不同使用dialog.Show()或dialog.ShowDialog()来呈现对话框
            if (Microsoft.Data.ConnectionUI.DataConnectionDialog.Show(connDialog) == DialogResult.OK)
            {
                
                if (connDialog.SelectedDataSource == Microsoft.Data.ConnectionUI.DataSource.SqlDataSource)
                {
                    NewSetting.Type = DbEngineSetting.ConnectionType.SQLCLIENT;
                }
                else
                {
                    NewSetting.Type = DbEngineSetting.ConnectionType.ODBC;
                }
                
                NewSetting.ConnectString = connDialog.ConnectionString;
                Setting = NewSetting;
            }
            //return NewConnection;
            */
        }
    }
}
