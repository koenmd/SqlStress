using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace sqlstress.views
{
    public partial class RunnerView : UserControl
    {
        private StressRunner Runner { get; set; }

        //public string SchemeName {get;set;}
        private object uisyncobj = new object();

        public enum RunningStatus
        {
            RUNNING = 0,
            NOTRUNNING = 1,
        }

        public enum ZomUnit
        {
            millisecond,
            microsecond,
            nanosecond
        }

        public RunningStatus Status;
        private ZomUnit Zomunit = ZomUnit.millisecond;
        public RunnerView()
        {
            InitializeComponent();

            tsMenuItemMm.Click += tsMenuItems_Click;
            tsMenuItemMs.Click += tsMenuItems_Click;
            tsMenuItemNs.Click += tsMenuItems_Click;

            Status = RunningStatus.NOTRUNNING;
            //Utils.Logger.onLog = onLogEvent;
            chartExecute.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            //chartExecute.ChartAreas[0].AxisX.ScaleView.Size = 60;
            chartExecute.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chartExecute.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            //chartExecute.ChartAreas[1].AxisX.LabelStyle.Format = "HH:mm:ss";
            //chartExecute.ChartAreas[1].AxisX.ScrollBar.IsPositionedInside = true;
            //chartExecute.ChartAreas[1].AxisX.ScrollBar.Enabled = true;
        }

        public void SetScheme(StressScheme _Scheme, perfcount counter)
        {
            Runner = new StressRunner(_Scheme);
            Runner.Counter = counter;
            Runner.OnFinished += (x, y) => {
                if (this.InvokeRequired)
                {
                    Action a = new Action(Stop);
                    this.Invoke(a, null);
                }
                else
                {
                    Stop();
                }
                Utils.Logger.Trace(new Exception(Resource.STRESSDONE), false, 0);
            };
            chartExecute.Series[0].Color = _Scheme.PaintColor;
        }

        public bool CheckRunner()
        {
            return Runner != null;
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (!CheckRunner())
            {
                return;
            }

            UpdateShowCounters();
            UpdateShowRunner();

            if (tsMenuItemMm.Checked) Zomunit = ZomUnit.millisecond;
            else if (tsMenuItemMm.Checked) Zomunit = ZomUnit.microsecond;
            else if (tsMenuItemNs.Checked) Zomunit = ZomUnit.nanosecond;
        }

        /*
        private void btSet_Click(object sender, EventArgs e)
        {
            if (!CheckRunner())
            {
                return;
            }

            if (WindowHelp.ShowSettings(Runner.Scheme, this) == DialogResult.OK)
            {
                SetScheme(Runner.Scheme.scheme, Runner.Counter);
            }
        }
        */

        public void UpdateShowCounters()
        {
            chartExecute.Series[0].Points.AddXY(DateTime.Now, Runner.Perfmon_ReqDone_PS);
            //chartExecute.ChartAreas[0].AxisX.ScaleView.Position = chartExecute.Series[0].Points.Count - 5; 
            switch (this.Zomunit)
            {
                case ZomUnit.millisecond:
                    chartExecute.Series[1].Points.AddXY(DateTime.Now, Runner.Perfmon_Time_PD);
                    break;
                case ZomUnit.microsecond:
                    chartExecute.Series[1].Points.AddXY(DateTime.Now, Runner.Perfmon_Time_PD * 1000);
                    break;
                case ZomUnit.nanosecond:
                    chartExecute.Series[1].Points.AddXY(DateTime.Now, Runner.Perfmon_Time_PD * 1000 * 1000);
                    break;
            }
            
            //chartTime.ChartAreas[0].AxisX.ScaleView.Position = chartTime.Series[0].Points.Count - 5; 
        }

        public void UpdateShowRunner()
        {
            if (Runner.Engine.Workers == null) return;
            foreach (SQLStressEngine.WorkerInfo wi in Runner.Engine.Workers)
            {
                string key = wi.index.ToString();
                if (!lvWorkerStatus.Items.ContainsKey(key))
                {
                    lock (uisyncobj)
                    {
                        ListViewItem it = lvWorkerStatus.Items.Add(key, key, -1);
                        it.SubItems.Add(wi.workcount.donecount.ToString());
                        it.SubItems.Add(wi.workcount.time_max.ToString());
                        it.SubItems.Add((Math.Round(wi.workcount.time_total / (wi.workcount.donecount - wi.workcount.errorcount + 0.001), 2)).ToString());
                        it.SubItems.Add(wi.workcount.errorcount.ToString());
                        it.SubItems.Add(wi.workcount.lastjob);
                    }
                }
                else
                {
                    lvWorkerStatus.Items[key].SubItems[1].Text = wi.workcount.donecount.ToString();
                    lvWorkerStatus.Items[key].SubItems[2].Text = wi.workcount.time_max.ToString();
                    lvWorkerStatus.Items[key].SubItems[3].Text = (Math.Round(wi.workcount.time_total / (wi.workcount.donecount - wi.workcount.errorcount + 0.001), 2)).ToString();
                    lvWorkerStatus.Items[key].SubItems[4].Text = wi.workcount.errorcount.ToString();
                    lvWorkerStatus.Items[key].SubItems[5].Text = wi.workcount.lastjob;
                }
            }
        }

        public void Run()
        {
            Status = RunningStatus.RUNNING;
            try
            {
                lvWorkerStatus.Items.Clear();
                Runner.StartRun();
                timerUpdate.Enabled = true;
                //btStart.Text = Resource.BTNTITLESTOP;
            }
            catch (System.Exception)
            {
                Status = RunningStatus.NOTRUNNING;
            }
            //btSet.Enabled = Status == RunningStatus.NOTRUNNING;
        }

        public void Stop()
        {
            if (Status == RunningStatus.NOTRUNNING)
            {
                return;
            }

            Runner.StopRun();
            Status = RunningStatus.NOTRUNNING;
            timerUpdate_Tick(this, new EventArgs());
            timerUpdate.Enabled = false;
            //btStart.Text = Resource.BTNTITLESTART;
            //btSet.Enabled = Status == RunningStatus.NOTRUNNING;
        }

        public void RunScheme()
        {
            if (!CheckRunner())
            {
                return;
            }

            if (Status == RunningStatus.NOTRUNNING)
            {
                Run();
            }
            else
            {
                Stop();
            }
        }


        private void btOpenCounter_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.GetFullPath(".\\Perfmon.PerfmonCfg")))
            {
                Utils.ToolBox.ProcessOpenFile(Path.GetFullPath(".\\Perfmon.PerfmonCfg"), "");
            }
            else
            {
                Utils.ToolBox.ProcessOpenFile("", "Perfmon");
            }
        }

        private void RunnerView_SizeChanged(object sender, EventArgs e)
        {
            if (lvWorkerStatus.Columns.Count <= 0) return;
            foreach (ColumnHeader col in lvWorkerStatus.Columns)
            {
                col.Width = Math.Abs(lvWorkerStatus.Width - 24) / lvWorkerStatus.Columns.Count;
            }
        }

        private void tbOpenLog_Click(object sender, EventArgs e)
        {
            if (File.Exists(Utils.Logger.GetLogFullPath))
            {
                Utils.ToolBox.ProcessOpenFile("", Utils.Logger.GetLogFullPath);
            }
        }

        private void tbSnapShot_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            sfdlg.FileName = string.Format("{0}.bmp", Runner.Scheme.SchemeName);
            if (sfdlg.ShowDialog() == DialogResult.OK)
            {
                chartExecute.SaveImage(sfdlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }

        private void tsMenuItems_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                foreach (ToolStripMenuItem mi in tbZomUnit.DropDownItems)
                {
                    mi.Checked = false;
                }
                ((ToolStripMenuItem)sender).Checked = true;
            }
        }

        private void tbClear_Click(object sender, EventArgs e)
        {
            chartExecute.Series[0].Points.Clear();
            chartExecute.Series[1].Points.Clear();
        }
    }
}
