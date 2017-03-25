using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using Utils;

namespace sqlstress
{
    public partial class FormMain : Form
    {
        public perfcount Counter = new perfcount();
        public static StressScheme CurrentScheme = null;
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Logger.onLog += onLogEvent;
            this.LoadSchemes();
        }

        private void LoadSchemes()
        {
            if (!Directory.Exists(Resource.SCHEMEPATH))
            {
                Directory.CreateDirectory(Resource.SCHEMEPATH);
            }

            tbComboBoxCase.Items.Clear();
            foreach (string folder in Utils.ToolBox.EnumPath(Resource.SCHEMEPATH, "."))
            {
                tbComboBoxCase.Items.Add(Path.GetFileNameWithoutExtension(folder));
            }

            tbComboBoxCase.SelectedIndex = (tbComboBoxCase.Items.Count > 0 ? 0 : -1);
            if (tbComboBoxCase.Items.Count == 0)
            {
                vrun.Enabled = false;
            }
        }


        private void tbComboBoxCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbComboBoxCase.Text == "")
            {
                vrun.Enabled = false;
                return;
            }

            //vrun.SetScheme(tbComboBoxCase.Text, Counter);
            CurrentScheme = new StressScheme(tbComboBoxCase.Text);
            vrun.SetScheme(CurrentScheme, Counter);
            vcase.Scheme = CurrentScheme;
        }

        private void tbButtonSave_Click(object sender, EventArgs e)
        {
            CurrentScheme.Save();
            CurrentScheme.Load();
            vrun.SetScheme(CurrentScheme, Counter);
            vcase.Scheme = CurrentScheme;
        }

        private void tbButtonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format(Resource.DIAG_DELETESURE, CurrentScheme.SchemeName), Resource.DIAG_DELETETILE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CurrentScheme.Delete();
                LoadSchemes();
            }
        }

        private void tbButtonNew_Click(object sender, EventArgs e)
        {
            StressScheme newscheme = new StressScheme();

            String NewSchemeName = Interaction.InputBox(Resource.NEWSCHEMEDIALOG, Resource.DIAG_NEWTILE, "NewScheme");
            if ((NewSchemeName == string.Empty) || (NewSchemeName.IndexOfAny(Path.GetInvalidPathChars()) >= 0))
            {
                MessageBox.Show(Resource.NEWSCHEMEERRMSG, Resource.MSGERRTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            newscheme.SchemeName = NewSchemeName;
            newscheme.Save();

            LoadSchemes();
            tbComboBoxCase.SelectedIndex = tbComboBoxCase.Items.IndexOf(NewSchemeName);
        }

        private void tbButtonRun_Click(object sender, EventArgs e)
        {
            tsslLastException.Text = "";
            vrun.RunScheme();
        }

        private void tbButtonStop_Click(object sender, EventArgs e)
        {
            vrun.Stop();
        }

        private void timerUi_Tick(object sender, EventArgs e)
        {
            tbButtonRun.Enabled = vrun.Status == views.RunnerView.RunningStatus.NOTRUNNING;
            tbButtonStop.Enabled = vrun.Status == views.RunnerView.RunningStatus.RUNNING;
            menuItemRun.Enabled = tbButtonRun.Enabled;
            menuItemStop.Enabled = tbButtonStop.Enabled;
            if (CurrentScheme != null)
            {
                tsProgressBar.Value = CurrentScheme.run_maxtimes != 0 ? (int)(100L * CurrentScheme.run_feed / CurrentScheme.run_maxtimes) : 0;
            }
            
            vcase.Enabled = vrun.Status == views.RunnerView.RunningStatus.NOTRUNNING;
            tbButtonNew.Enabled = vrun.Status == views.RunnerView.RunningStatus.NOTRUNNING;
            tbButtonSave.Enabled = vrun.Status == views.RunnerView.RunningStatus.NOTRUNNING; 
            tbButtonDelete.Enabled = vrun.Status == views.RunnerView.RunningStatus.NOTRUNNING;
            tbComboBoxCase.Enabled = vrun.Status == views.RunnerView.RunningStatus.NOTRUNNING;
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            vrun.Stop();
            this.Close();
        }

        private void onLogEvent(int logtype, string message, bool raise)
        {
            if (!this.InvokeRequired)
            {
                if (!(tsslLastException.Tag is long)) tsslLastException.Tag = 0L;
                if ((DateTime.Now.Ticks - (long)tsslLastException.Tag) > 1000L * 1000 * 10)
                {
                    tsslLastException.ForeColor = logtype == 0 ? Color.Black : Color.Red;
                    tsslLastException.Text = message;
                }

                if (raise)
                {
                    string msgtext = message;
                    MessageBox.Show(message, Resource.MSGERRTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Utils.Logger.delegateLogEvent dlg = new Utils.Logger.delegateLogEvent(onLogEvent);
                this.Invoke(dlg, logtype, message, raise);
            }
        }
    }    
}
