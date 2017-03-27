using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sqlstress.dialog
{
    public partial class dlgParamBuilder : Form
    {
        private sqlexpression sqlexp;
        public dlgParamBuilder(sqlexpression _sqlexp)
        {
            InitializeComponent();
            sqlexp = _sqlexp;
        }

        protected override void OnLoad(EventArgs e)
        {
            //winAero aero = new winAero(this);
            //aero.OpenAero();
            base.OnLoad(e);
        }

        private void btPreview_Click(object sender, EventArgs e)
        {
            sqlexpression newsqlexp = new sqlexpression();
            newsqlexp.ParamsData = GenerateParam();

            edResult.Clear();
            edResult.Text = newsqlexp.ParamText;
        }

        private DataTable GenerateParam()
        {
            return datapicker.Data;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            sqlexp.ParamsData = GenerateParam();
            //this.DialogResult = DialogResult.OK;
        }
    }
}
