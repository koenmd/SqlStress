namespace sqlstress.dailog
{
    partial class DatapickView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatapickView));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edpickexp = new ScintillaNET.Scintilla();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPickresult = new System.Windows.Forms.DataGridView();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btPickup = new System.Windows.Forms.Button();
            this.tbBrowse = new System.Windows.Forms.Button();
            this.tstbSource = new System.Windows.Forms.TextBox();
            this.rbRegex = new System.Windows.Forms.RadioButton();
            this.rbDatabase = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickresult)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 53);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(601, 254);
            this.splitContainer1.SplitterDistance = 298;
            this.splitContainer1.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edpickexp);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 254);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "提取条件";
            // 
            // edpickexp
            // 
            this.edpickexp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edpickexp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edpickexp.Lexer = ScintillaNET.Lexer.Sql;
            this.edpickexp.Location = new System.Drawing.Point(3, 17);
            this.edpickexp.Name = "edpickexp";
            this.edpickexp.Size = new System.Drawing.Size(292, 234);
            this.edpickexp.TabIndex = 18;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPickresult);
            this.groupBox2.Controls.Add(this.progress);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 254);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "提取结果";
            // 
            // dgvPickresult
            // 
            this.dgvPickresult.BackgroundColor = System.Drawing.Color.Gray;
            this.dgvPickresult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPickresult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPickresult.Location = new System.Drawing.Point(3, 17);
            this.dgvPickresult.Name = "dgvPickresult";
            this.dgvPickresult.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPickresult.RowTemplate.Height = 21;
            this.dgvPickresult.Size = new System.Drawing.Size(293, 217);
            this.dgvPickresult.TabIndex = 17;
            // 
            // progress
            // 
            this.progress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progress.Location = new System.Drawing.Point(3, 234);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(293, 17);
            this.progress.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btPickup);
            this.panel2.Controls.Add(this.tbBrowse);
            this.panel2.Controls.Add(this.tstbSource);
            this.panel2.Controls.Add(this.rbRegex);
            this.panel2.Controls.Add(this.rbDatabase);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(601, 53);
            this.panel2.TabIndex = 13;
            // 
            // btPickup
            // 
            this.btPickup.Dock = System.Windows.Forms.DockStyle.Right;
            this.btPickup.Location = new System.Drawing.Point(501, 0);
            this.btPickup.Name = "btPickup";
            this.btPickup.Size = new System.Drawing.Size(100, 53);
            this.btPickup.TabIndex = 6;
            this.btPickup.Text = "提取";
            this.btPickup.UseVisualStyleBackColor = true;
            this.btPickup.Click += new System.EventHandler(this.btPickup_Click);
            // 
            // tbBrowse
            // 
            this.tbBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.tbBrowse.Image = ((System.Drawing.Image)(resources.GetObject("tbBrowse.Image")));
            this.tbBrowse.Location = new System.Drawing.Point(390, 28);
            this.tbBrowse.Name = "tbBrowse";
            this.tbBrowse.Size = new System.Drawing.Size(29, 19);
            this.tbBrowse.TabIndex = 5;
            this.tbBrowse.UseVisualStyleBackColor = true;
            this.tbBrowse.Click += new System.EventHandler(this.tbBrowse_Click);
            // 
            // tstbSource
            // 
            this.tstbSource.BackColor = System.Drawing.SystemColors.Control;
            this.tstbSource.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tstbSource.Location = new System.Drawing.Point(83, 28);
            this.tstbSource.Name = "tstbSource";
            this.tstbSource.ReadOnly = true;
            this.tstbSource.Size = new System.Drawing.Size(301, 20);
            this.tstbSource.TabIndex = 4;
            // 
            // rbRegex
            // 
            this.rbRegex.AutoSize = true;
            this.rbRegex.Location = new System.Drawing.Point(160, 9);
            this.rbRegex.Name = "rbRegex";
            this.rbRegex.Size = new System.Drawing.Size(83, 16);
            this.rbRegex.TabIndex = 3;
            this.rbRegex.Text = "正则表达式";
            this.rbRegex.UseVisualStyleBackColor = true;
            this.rbRegex.CheckedChanged += new System.EventHandler(this.rbRegex_CheckedChanged);
            // 
            // rbDatabase
            // 
            this.rbDatabase.AutoSize = true;
            this.rbDatabase.Checked = true;
            this.rbDatabase.Location = new System.Drawing.Point(83, 9);
            this.rbDatabase.Name = "rbDatabase";
            this.rbDatabase.Size = new System.Drawing.Size(59, 16);
            this.rbDatabase.TabIndex = 2;
            this.rbDatabase.TabStop = true;
            this.rbDatabase.Text = "数据集";
            this.rbDatabase.UseVisualStyleBackColor = true;
            this.rbDatabase.CheckedChanged += new System.EventHandler(this.rbDatabase_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据来源：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "提取方式：";
            // 
            // DatapickView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Name = "DatapickView";
            this.Size = new System.Drawing.Size(601, 307);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickresult)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ScintillaNET.Scintilla edpickexp;
        private System.Windows.Forms.DataGridView dgvPickresult;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btPickup;
        private System.Windows.Forms.Button tbBrowse;
        private System.Windows.Forms.TextBox tstbSource;
        private System.Windows.Forms.RadioButton rbRegex;
        private System.Windows.Forms.RadioButton rbDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ProgressBar progress;
    }
}
