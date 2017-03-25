using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;

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
}
