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
    public partial class dlgSqlBuilder : Form
    {
        private sqlserials sqlexp;
        public dlgSqlBuilder(sqlserials _sqlexp)
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
            edResult.Clear();
            edResult.Text = GenerateSQL();
        }

        private string GenerateSQL()
        {            
            string result = "";

            //string[] regcolumns = SchemeRunner.getsqlparams(tbSQLModel.Text);
            string sqlmodel = sqlexp.SqlText.Trim();
            //sqlmodel += sqlmodel[sqlmodel.Length - 1] == ';' ? "\r\n" : ";\r\n";
            string newsql = "";
            string colname = "";
            datapicker.progress.Maximum = datapicker.Data.Rows.Count;
            datapicker.progress.Value = 0;

            foreach (DataRow row in datapicker.Data.Rows)
            {
                newsql = sqlmodel;
                foreach (DataColumn col in datapicker.Data.Columns)
                {
                    colname = col.ColumnName[0] == '@' ? col.ColumnName : '@' + col.ColumnName;
                    if (row[col.ColumnName] != null)
                    {
                        newsql = newsql.Replace(colname, row[col.ColumnName].ToString());
                    }
                }
                result = result + newsql;
                datapicker.progress.Value++;
            }

            return result;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            sqlexp.SqlText = GenerateSQL();
            //this.DialogResult = DialogResult.OK;
        }
    }
}
