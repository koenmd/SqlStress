namespace sqlstress.views
{
    partial class RunnerView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunnerView));
            this.splitContainerRunner = new System.Windows.Forms.SplitContainer();
            this.chartExecute = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.btOpenCounter = new System.Windows.Forms.ToolStripButton();
            this.tbOpenLog = new System.Windows.Forms.ToolStripButton();
            this.tbZomUnit = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsMenuItemMs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemMm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemNs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarButtonSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbSnapShot = new System.Windows.Forms.ToolStripButton();
            this.lvWorkerStatus = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.tbClear = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRunner)).BeginInit();
            this.splitContainerRunner.Panel1.SuspendLayout();
            this.splitContainerRunner.Panel2.SuspendLayout();
            this.splitContainerRunner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartExecute)).BeginInit();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerRunner
            // 
            this.splitContainerRunner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRunner.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRunner.Name = "splitContainerRunner";
            this.splitContainerRunner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRunner.Panel1
            // 
            this.splitContainerRunner.Panel1.Controls.Add(this.chartExecute);
            this.splitContainerRunner.Panel1.Controls.Add(this.toolBar);
            // 
            // splitContainerRunner.Panel2
            // 
            this.splitContainerRunner.Panel2.Controls.Add(this.lvWorkerStatus);
            this.splitContainerRunner.Size = new System.Drawing.Size(431, 454);
            this.splitContainerRunner.SplitterDistance = 350;
            this.splitContainerRunner.TabIndex = 0;
            // 
            // chartExecute
            // 
            this.chartExecute.BackColor = System.Drawing.SystemColors.Window;
            this.chartExecute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.chartExecute.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            this.chartExecute.BorderlineWidth = 0;
            this.chartExecute.BorderSkin.BackColor = System.Drawing.Color.Snow;
            this.chartExecute.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            this.chartExecute.BorderSkin.BorderWidth = 0;
            chartArea1.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Lines;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Lines;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.CursorX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            chartArea1.CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 80F;
            chartArea1.InnerPlotPosition.Width = 82F;
            chartArea1.InnerPlotPosition.X = 10F;
            chartArea1.InnerPlotPosition.Y = 10F;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "ChartArea1";
            this.chartExecute.ChartAreas.Add(chartArea1);
            this.chartExecute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartExecute.Location = new System.Drawing.Point(0, 25);
            this.chartExecute.Name = "chartExecute";
            this.chartExecute.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.BorderWidth = 0;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Color = System.Drawing.Color.LightGreen;
            series1.IsVisibleInLegend = false;
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series1.YValuesPerPoint = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.Name = "Series2";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series2.YValuesPerPoint = 2;
            this.chartExecute.Series.Add(series1);
            this.chartExecute.Series.Add(series2);
            this.chartExecute.Size = new System.Drawing.Size(431, 325);
            this.chartExecute.TabIndex = 11;
            this.chartExecute.Text = "chart1";
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btOpenCounter,
            this.tbOpenLog,
            this.tbZomUnit,
            this.tbClear,
            this.toolBarButtonSeparator1,
            this.tbSnapShot});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(431, 25);
            this.toolBar.TabIndex = 10;
            // 
            // btOpenCounter
            // 
            this.btOpenCounter.Image = ((System.Drawing.Image)(resources.GetObject("btOpenCounter.Image")));
            this.btOpenCounter.Name = "btOpenCounter";
            this.btOpenCounter.Size = new System.Drawing.Size(88, 22);
            this.btOpenCounter.Text = "查看计数器";
            this.btOpenCounter.ToolTipText = "Show Layout From XML";
            this.btOpenCounter.Click += new System.EventHandler(this.btOpenCounter_Click);
            // 
            // tbOpenLog
            // 
            this.tbOpenLog.Image = ((System.Drawing.Image)(resources.GetObject("tbOpenLog.Image")));
            this.tbOpenLog.Name = "tbOpenLog";
            this.tbOpenLog.Size = new System.Drawing.Size(76, 22);
            this.tbOpenLog.Text = "查看日志";
            this.tbOpenLog.ToolTipText = "Open";
            this.tbOpenLog.Click += new System.EventHandler(this.tbOpenLog_Click);
            // 
            // tbZomUnit
            // 
            this.tbZomUnit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItemMs,
            this.tsMenuItemMm,
            this.tsMenuItemNs});
            this.tbZomUnit.Image = ((System.Drawing.Image)(resources.GetObject("tbZomUnit.Image")));
            this.tbZomUnit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbZomUnit.Name = "tbZomUnit";
            this.tbZomUnit.Size = new System.Drawing.Size(85, 22);
            this.tbZomUnit.Text = "显示单位";
            // 
            // tsMenuItemMs
            // 
            this.tsMenuItemMs.Checked = true;
            this.tsMenuItemMs.CheckOnClick = true;
            this.tsMenuItemMs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsMenuItemMs.Name = "tsMenuItemMs";
            this.tsMenuItemMs.Size = new System.Drawing.Size(100, 22);
            this.tsMenuItemMs.Text = "毫秒";
            // 
            // tsMenuItemMm
            // 
            this.tsMenuItemMm.CheckOnClick = true;
            this.tsMenuItemMm.Name = "tsMenuItemMm";
            this.tsMenuItemMm.Size = new System.Drawing.Size(100, 22);
            this.tsMenuItemMm.Text = "微秒";
            // 
            // tsMenuItemNs
            // 
            this.tsMenuItemNs.CheckOnClick = true;
            this.tsMenuItemNs.Name = "tsMenuItemNs";
            this.tsMenuItemNs.Size = new System.Drawing.Size(100, 22);
            this.tsMenuItemNs.Text = "纳秒";
            // 
            // toolBarButtonSeparator1
            // 
            this.toolBarButtonSeparator1.Name = "toolBarButtonSeparator1";
            this.toolBarButtonSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbSnapShot
            // 
            this.tbSnapShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbSnapShot.Image = ((System.Drawing.Image)(resources.GetObject("tbSnapShot.Image")));
            this.tbSnapShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSnapShot.Name = "tbSnapShot";
            this.tbSnapShot.Size = new System.Drawing.Size(23, 22);
            this.tbSnapShot.Text = "保存";
            this.tbSnapShot.Click += new System.EventHandler(this.tbSnapShot_Click);
            // 
            // lvWorkerStatus
            // 
            this.lvWorkerStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvWorkerStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvWorkerStatus.Location = new System.Drawing.Point(0, 0);
            this.lvWorkerStatus.Name = "lvWorkerStatus";
            this.lvWorkerStatus.Size = new System.Drawing.Size(431, 100);
            this.lvWorkerStatus.TabIndex = 1;
            this.lvWorkerStatus.UseCompatibleStateImageBehavior = false;
            this.lvWorkerStatus.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "线程";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "请求数";
            this.columnHeader2.Width = 77;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "最大耗时(毫秒)";
            this.columnHeader3.Width = 74;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "平均耗时(毫秒)";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "错误数";
            this.columnHeader5.Width = 76;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "当前任务";
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // tbClear
            // 
            this.tbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbClear.Image = ((System.Drawing.Image)(resources.GetObject("tbClear.Image")));
            this.tbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbClear.Name = "tbClear";
            this.tbClear.Size = new System.Drawing.Size(23, 22);
            this.tbClear.Text = "清除";
            this.tbClear.Click += new System.EventHandler(this.tbClear_Click);
            // 
            // RunnerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerRunner);
            this.Name = "RunnerView";
            this.Size = new System.Drawing.Size(431, 454);
            this.SizeChanged += new System.EventHandler(this.RunnerView_SizeChanged);
            this.splitContainerRunner.Panel1.ResumeLayout(false);
            this.splitContainerRunner.Panel1.PerformLayout();
            this.splitContainerRunner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRunner)).EndInit();
            this.splitContainerRunner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartExecute)).EndInit();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerRunner;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton btOpenCounter;
        private System.Windows.Forms.ToolStripButton tbOpenLog;
        private System.Windows.Forms.ToolStripSeparator toolBarButtonSeparator1;
        private System.Windows.Forms.ToolStripButton tbSnapShot;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartExecute;
        private System.Windows.Forms.ListView lvWorkerStatus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.ToolStripDropDownButton tbZomUnit;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemMs;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemMm;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemNs;
        private System.Windows.Forms.ToolStripButton tbClear;
    }
}
