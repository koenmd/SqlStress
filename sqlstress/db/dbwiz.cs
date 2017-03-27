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
                IDbEngine tempEngine = (IDbEngine)t.Assembly.CreateInstance(t.FullName);
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
            catch (Exception)
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
                    IDbEngine tempEngine = (IDbEngine)EnginType.Assembly.CreateInstance(EnginType.FullName);
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
        }
    }
}
