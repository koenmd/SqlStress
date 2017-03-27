using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace sqlstress
{
    public partial class paramseditor : Form
    {
        public sqlexpression sqlexp
        {
            get { return _sqlexp; }
            set { setsqlexp(value); }
        }   private sqlexpression _sqlexp = new sqlexpression();

        private DataTable ParamsTable = new DataTable();
        private sqleditcontrol lexer = null;

        public paramseditor()
        {
            InitializeComponent();
            lexer = new sqleditcontrol(edSql);
        }

        private void setsqlexp(sqlexpression value)
        {
            _sqlexp = value;
            edSql.Text = _sqlexp.SqlText;
            edSql.ReadOnly = true;
            showParams();
        }

        private void showParams()
        {
            dgvParams.DataSource = null;
            /*
            dgvParams.Rows.Clear();
            dgvParams.Columns.Clear();

            foreach (string paramname in sqlexp.ParamNames)
            {
                int i = dgvParams.Columns.Add(paramname, "");
                dgvParams.Columns[i].HeaderText = paramname;
            }
            */
            /*
            ParamsTable.Rows.Clear();
            ParamsTable.Columns.Clear();

            foreach (string paramname in sqlexp.ParamNames)
            {
                DataColumn dc = ParamsTable.Columns.Add(paramname);
            }

            foreach (sqlstatement st in sqlexp)
            {
                DataRow row = ParamsTable.NewRow();
                foreach (KeyValuePair<string, string> cv in st.sqlparams)
                {
                    row[cv.Key] = cv.Value;
                }
            }
            */
            ParamsTable = sqlexp.ParamsData;
            dgvParams.DataSource = ParamsTable;
        }

        private void paramseditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            sqlexp.ParamsData = ParamsTable;
        }

        private void tsbParamsBuild_Click(object sender, EventArgs e)
        {
            using (dialog.dlgParamBuilder builder = new dialog.dlgParamBuilder(sqlexp))
            {
                builder.Visible = false;
                builder.ShowDialog(this);
                showParams();
            }
        }

        private void tsbSQLImport_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofdlg.Multiselect = false;
            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                edSql.Text = Utils.ToolBox.FileToString(ofdlg.FileName);
            }
            */
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            sqlexp.ParamText = "";
            showParams();
        }
    }

    public class sqlparamsditor : UITypeEditor
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
                sqlexpression sqle = (sqlexpression)context.Instance;
                using (paramseditor editor = new paramseditor())
                {
                    editor.sqlexp = sqle;
                    editor.ShowDialog();
                    value = editor.sqlexp.ParamText;
                }
            }
            return value;
        }
    }
}
