using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace sqlstress
{
    public class dbconneditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            //指定为模式窗体属性编辑器类型 
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //打开属性编辑器修改数据 
            if (context.Instance != null)
            {
                if (value != null)
                {
                    DbEngineSetting setting = (DbEngineSetting)value;
                    setting.ShowWizard();
                    //return value;

                    StressScheme Scheme = (StressScheme)context.Instance;
                    Scheme.dbsettingshelper.Current = null;
                }
                else
                {
                    DbEngineSetting setting = new DbEngineSetting();
                    setting.ShowWizard();
                    value = setting;
                }
            }
            return value;
        }
    }

    public class dbusualconneditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            //指定为模式窗体属性编辑器类型 
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (service == null || context.Instance == null)
                {
                    return value;
                }

                //StressScheme scheme = 
                EnginSettingsHelper setingshelper = ((StressScheme)context.Instance).dbsettingshelper;
                setingshelper.Scheme = (StressScheme)context.Instance;
                if (setingshelper == null)
                {
                    return value;
                }

                ListBox valueitems = new ListBox();
                valueitems.BorderStyle = BorderStyle.None;
                valueitems.Font = new System.Drawing.Font("Consolas", 9);
                foreach (UsualSetting item in setingshelper)
                {
                    valueitems.Items.Add(item.DisplayName);
                }

                valueitems.SelectedIndexChanged += (sender, e) =>
                {
                    setingshelper.Current = setingshelper.GetSettingByName(valueitems.Text);
                };

                valueitems.Click += (sender, e) =>
                {
                    service.CloseDropDown();
                }; 

                service.DropDownControl(valueitems);
                value = setingshelper;
            }
            return value;
        }

        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }
    }
}
