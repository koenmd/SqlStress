using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace sqlstress.dialog
{
    public partial class DatapickView : UserControl
    {
        public DataTable Data { get; private set; } = new DataTable();
        private enum PickType
        {
            SQL = 0,
            REGEX = 1,
        }

        private PickType SourceType = PickType.SQL;
        private DbEngineSetting Dbconn = new DbEngineSetting();
        private string FileName;

        private sqleditcontrol Lexer = null;
        public DatapickView()
        {
            InitializeComponent();
            //tscbSourceType.SelectedIndex = 0;
            Lexer = new sqleditcontrol(edpickexp);
            edpickexp.WrapMode = ScintillaNET.WrapMode.None;
            if (FormMain.CurrentScheme != null)
            {
                rbDatabase.Checked = true;
                Dbconn = FormMain.CurrentScheme.dbsettings;
                tstbSource.Text = Dbconn.ToString();
            }
        }

        private void tbBrowse_Click(object sender, EventArgs e)
        {
            switch (SourceType)
            {
                case PickType.SQL:
                    Dbconn.ShowWizard();
                    tstbSource.Text = Dbconn.ToString();
                    break;
                case PickType.REGEX:
                    OpenFileDialog ofdlg = new OpenFileDialog();
                    ofdlg.Filter = "text files (*.txt)|*.txt|All files (*.*)|*.*";
                    ofdlg.Multiselect = false;
                    if (ofdlg.ShowDialog() == DialogResult.OK)
                    {
                        FileName = ofdlg.FileName;
                        tstbSource.Text = FileName;
                    }
                    break;
            }
        }

        private void btPickup_Click(object sender, EventArgs e)
        {
            switch (SourceType)
            {
                case PickType.SQL:
                    string sql = edpickexp.Text;
                    if (sql == "") return;

                    dgvPickresult.DataSource = null;

                    progress.Value = 0;
                    DbRunner sqlr = new DbRunner(Dbconn);
                    this.Data = sqlr.Run_WithResult(sql);
                    
                    progress.Value = 100;
                    dgvPickresult.DataSource = this.Data;
                    break;
                case PickType.REGEX:
                    PickFromFile();
                    break;
            }
        }

        void PickFromFile()
        {
            string regexstr = edpickexp.Text;
            if (regexstr == "") return;

            if (FileName == "") return;

            Regex r = new Regex(regexstr);
            string colname = "";
            string[] columns = r.GetGroupNames();

            dgvPickresult.DataSource = null;

            this.Data.Clear();
            this.Data.Columns.Clear();
            foreach (string col in columns)
            {
                if (col != "0")
                {
                    colname = col[0] == '@' ? col.Substring(1, col.Length - 1) : col;
                    this.Data.Columns.Add(colname);
                }
            }

            string[] filelines = File.ReadAllLines(FileName);
            progress.Maximum = filelines.Length;
            progress.Value = 0;

            foreach (string text in filelines)
            {
                try
                {
                    DataRow row = this.Data.NewRow();
                    var matches = r.Matches(text);
                    foreach (Match m in matches)
                    {
                        for (int i = 1; i < m.Groups.Count; i++)
                        {
                            row[i] = m.Groups[i].Value;
                        }                        
                    }
                    this.Data.Rows.Add(row);
                }
                catch (System.Exception ex)
                {
                    Utils.Logger.Trace(ex);
                }
                progress.Value++;
            }
            this.Data.AcceptChanges();

            dgvPickresult.DataSource = this.Data;
        }

        private void rbDatabase_CheckedChanged(object sender, EventArgs e)
        {
            this.SourceType = PickType.SQL;
        }

        private void rbRegex_CheckedChanged(object sender, EventArgs e)
        {
            this.SourceType = PickType.REGEX;
        }
    }
}
