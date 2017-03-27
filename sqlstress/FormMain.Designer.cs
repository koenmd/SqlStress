namespace sqlstress
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.tbButtonNew = new System.Windows.Forms.ToolStripButton();
            this.tbButtonSave = new System.Windows.Forms.ToolStripButton();
            this.tbButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tbLabel = new System.Windows.Forms.ToolStripLabel();
            this.tbComboBoxCase = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbButtonRun = new System.Windows.Forms.ToolStripButton();
            this.tbButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbUsualSettings = new System.Windows.Forms.ToolStripButton();
            this.tbButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuItemCase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRun = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslNetSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLastException = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.timerUi = new System.Windows.Forms.Timer(this.components);
            this.vcase = new sqlstress.views.CaseView();
            this.vrun = new sqlstress.views.RunnerView();
            this.toolBar.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbButtonNew,
            this.tbButtonSave,
            this.tbButtonDelete,
            this.toolStripSeparator,
            this.tbLabel,
            this.tbComboBoxCase,
            this.toolStripSeparator1,
            this.tbButtonRun,
            this.tbButtonStop,
            this.toolStripSeparator2,
            this.tbUsualSettings,
            this.tbButtonHelp});
            this.toolBar.Location = new System.Drawing.Point(0, 25);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(811, 25);
            this.toolBar.TabIndex = 9;
            // 
            // tbButtonNew
            // 
            this.tbButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbButtonNew.Image = ((System.Drawing.Image)(resources.GetObject("tbButtonNew.Image")));
            this.tbButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbButtonNew.Name = "tbButtonNew";
            this.tbButtonNew.Size = new System.Drawing.Size(23, 22);
            this.tbButtonNew.Text = "新建(&N)";
            this.tbButtonNew.Click += new System.EventHandler(this.tbButtonNew_Click);
            // 
            // tbButtonSave
            // 
            this.tbButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("tbButtonSave.Image")));
            this.tbButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbButtonSave.Name = "tbButtonSave";
            this.tbButtonSave.Size = new System.Drawing.Size(23, 22);
            this.tbButtonSave.Text = "保存(&S)";
            this.tbButtonSave.Click += new System.EventHandler(this.tbButtonSave_Click);
            // 
            // tbButtonDelete
            // 
            this.tbButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("tbButtonDelete.Image")));
            this.tbButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbButtonDelete.Name = "tbButtonDelete";
            this.tbButtonDelete.Size = new System.Drawing.Size(23, 22);
            this.tbButtonDelete.Text = "删除(&U)";
            this.tbButtonDelete.Click += new System.EventHandler(this.tbButtonDelete_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tbLabel
            // 
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Size = new System.Drawing.Size(32, 22);
            this.tbLabel.Text = "样本";
            // 
            // tbComboBoxCase
            // 
            this.tbComboBoxCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbComboBoxCase.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tbComboBoxCase.Name = "tbComboBoxCase";
            this.tbComboBoxCase.Size = new System.Drawing.Size(200, 25);
            this.tbComboBoxCase.SelectedIndexChanged += new System.EventHandler(this.tbComboBoxCase_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbButtonRun
            // 
            this.tbButtonRun.Image = ((System.Drawing.Image)(resources.GetObject("tbButtonRun.Image")));
            this.tbButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbButtonRun.Name = "tbButtonRun";
            this.tbButtonRun.Size = new System.Drawing.Size(52, 22);
            this.tbButtonRun.Text = "启动";
            this.tbButtonRun.Click += new System.EventHandler(this.tbButtonRun_Click);
            // 
            // tbButtonStop
            // 
            this.tbButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("tbButtonStop.Image")));
            this.tbButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbButtonStop.Name = "tbButtonStop";
            this.tbButtonStop.Size = new System.Drawing.Size(52, 22);
            this.tbButtonStop.Text = "停止";
            this.tbButtonStop.Click += new System.EventHandler(this.tbButtonStop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tbUsualSettings
            // 
            this.tbUsualSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbUsualSettings.Image = ((System.Drawing.Image)(resources.GetObject("tbUsualSettings.Image")));
            this.tbUsualSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbUsualSettings.Name = "tbUsualSettings";
            this.tbUsualSettings.Size = new System.Drawing.Size(23, 22);
            this.tbUsualSettings.Text = "常用连接";
            this.tbUsualSettings.Click += new System.EventHandler(this.tbUsualSettings_Click);
            // 
            // tbButtonHelp
            // 
            this.tbButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbButtonHelp.Enabled = false;
            this.tbButtonHelp.Image = ((System.Drawing.Image)(resources.GetObject("tbButtonHelp.Image")));
            this.tbButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbButtonHelp.Name = "tbButtonHelp";
            this.tbButtonHelp.Size = new System.Drawing.Size(23, 22);
            this.tbButtonHelp.Text = "帮助(&L)";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCase,
            this.menuItemHelp});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(811, 25);
            this.mainMenu.TabIndex = 10;
            // 
            // menuItemCase
            // 
            this.menuItemCase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemRun,
            this.menuItemStop,
            this.toolStripSeparator3,
            this.menuItemNew,
            this.menuItemDelete,
            this.menuItemSave,
            this.menuItem4,
            this.menuItemExit});
            this.menuItemCase.Name = "menuItemCase";
            this.menuItemCase.Size = new System.Drawing.Size(48, 21);
            this.menuItemCase.Text = "&Case";
            // 
            // menuItemRun
            // 
            this.menuItemRun.Name = "menuItemRun";
            this.menuItemRun.Size = new System.Drawing.Size(113, 22);
            this.menuItemRun.Text = "&Run";
            this.menuItemRun.Click += new System.EventHandler(this.tbButtonRun_Click);
            // 
            // menuItemStop
            // 
            this.menuItemStop.Name = "menuItemStop";
            this.menuItemStop.Size = new System.Drawing.Size(113, 22);
            this.menuItemStop.Text = "&Stop";
            this.menuItemStop.Click += new System.EventHandler(this.tbButtonStop_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(110, 6);
            // 
            // menuItemNew
            // 
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.Size = new System.Drawing.Size(113, 22);
            this.menuItemNew.Text = "&New";
            this.menuItemNew.Click += new System.EventHandler(this.tbButtonNew_Click);
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.Name = "menuItemDelete";
            this.menuItemDelete.Size = new System.Drawing.Size(113, 22);
            this.menuItemDelete.Text = "&Delete";
            this.menuItemDelete.Click += new System.EventHandler(this.tbButtonDelete_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.Size = new System.Drawing.Size(113, 22);
            this.menuItemSave.Text = "&Save";
            this.menuItemSave.Click += new System.EventHandler(this.tbButtonSave_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Name = "menuItem4";
            this.menuItem4.Size = new System.Drawing.Size(110, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(113, 22);
            this.menuItemExit.Text = "&Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
            this.menuItemHelp.MergeIndex = 3;
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(47, 21);
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Enabled = false;
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(177, 22);
            this.menuItemAbout.Text = "&About SqlStress...";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgressBar,
            this.tsslNetSpeed,
            this.tsslLastException});
            this.statusBar.Location = new System.Drawing.Point(0, 464);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(811, 26);
            this.statusBar.TabIndex = 11;
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(246, 20);
            // 
            // tsslNetSpeed
            // 
            this.tsslNetSpeed.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslNetSpeed.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tsslNetSpeed.Name = "tsslNetSpeed";
            this.tsslNetSpeed.Size = new System.Drawing.Size(53, 21);
            this.tsslNetSpeed.Text = "0.0M/S";
            // 
            // tsslLastException
            // 
            this.tsslLastException.AutoSize = false;
            this.tsslLastException.Name = "tsslLastException";
            this.tsslLastException.Size = new System.Drawing.Size(495, 21);
            this.tsslLastException.Spring = true;
            this.tsslLastException.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.vcase);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.vrun);
            this.splitContainer1.Size = new System.Drawing.Size(811, 414);
            this.splitContainer1.SplitterDistance = 247;
            this.splitContainer1.TabIndex = 12;
            // 
            // timerUi
            // 
            this.timerUi.Enabled = true;
            this.timerUi.Interval = 500;
            this.timerUi.Tick += new System.EventHandler(this.timerUi_Tick);
            // 
            // vcase
            // 
            this.vcase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vcase.Location = new System.Drawing.Point(0, 0);
            this.vcase.Name = "vcase";
            this.vcase.Scheme = null;
            this.vcase.Size = new System.Drawing.Size(247, 414);
            this.vcase.TabIndex = 0;
            // 
            // vrun
            // 
            this.vrun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vrun.Location = new System.Drawing.Point(0, 0);
            this.vrun.Name = "vrun";
            this.vrun.Size = new System.Drawing.Size(560, 414);
            this.vrun.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 490);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.mainMenu);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQLStress";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripLabel tbLabel;
        private System.Windows.Forms.ToolStripComboBox tbComboBoxCase;
        private System.Windows.Forms.ToolStripButton tbButtonRun;
        private System.Windows.Forms.ToolStripButton tbButtonStop;
        private System.Windows.Forms.ToolStripMenuItem menuItemCase;
        private System.Windows.Forms.ToolStripMenuItem menuItemRun;
        private System.Windows.Forms.ToolStripMenuItem menuItemStop;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private views.CaseView vcase;
        private views.RunnerView vrun;
        private System.Windows.Forms.ToolStripButton tbButtonNew;
        private System.Windows.Forms.ToolStripButton tbButtonSave;
        private System.Windows.Forms.ToolStripButton tbButtonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbButtonHelp;
        private System.Windows.Forms.Timer timerUi;
        private System.Windows.Forms.ToolStripStatusLabel tsslNetSpeed;
        private System.Windows.Forms.ToolStripStatusLabel tsslLastException;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripSeparator menuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripButton tbUsualSettings;
    }
}