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
    public partial class sqleditor : Form
    {
        public sqlexpression sqlexp {
            get { return _sqlexp; }
            set { setsqlexp(value); }
        } private sqlexpression _sqlexp = new sqlexpression();

        private sqleditcontrol lexer = null;

        public sqleditor()
        {
            InitializeComponent();
            lexer = new sqleditcontrol(edSql);
            //this.edSql.Lexer = ScintillaNET.Lexer.Sql;
            //this.edSql.LexerLanguage = 
        }

        private void setsqlexp(sqlexpression value)
        {
            _sqlexp = value;
            edSql.Text = _sqlexp.SqlText;
            //edSql.IndicatorCurrent = 8;
            //edSql.IndicatorFillRange(0, 1000);
        }

        private void sqleditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            _sqlexp.SqlText = edSql.Text;
        }

        public string ShowLoadFile()
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Filter = "sql files (*.sql)|*.sql|All files (*.*)|*.*";
            ofdlg.Multiselect = false;
            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                return ofdlg.FileName;
            }
            return string.Empty;
        }

        private void tsbSQLImport_Click(object sender, EventArgs e)
        {
            string filename = ShowLoadFile();
            if (filename != string.Empty)
            {
                edSql.Text = Utils.ToolBox.FileToString(filename);
            }
        }

        private void tsbSQLBuild_Click(object sender, EventArgs e)
        {
            /*
            if (WindowHelp.ShowSqlTool(this.Scheme, SqlBuildView.GeneratorMode.GENSQL, this) == DialogResult.OK)
            {
                edSql.Text = Scheme.StressqlStr;
            }
            */
            using (dailog.dlgSqlBuilder builder = new dailog.dlgSqlBuilder(this.sqlexp))
            {
                builder.Visible = false;
                //Hide();
                builder.ShowDialog();
                edSql.Text = _sqlexp.SqlText;
            }
        }
    }

    public class sqlstringeditor : UITypeEditor
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
                using (sqleditor editor = new sqleditor())
                {
                    editor.sqlexp = sqle;
                    editor.ShowDialog();
                    value = editor.sqlexp.SqlText;
                }
            }
            return value;
        }
    }
}
