namespace sqlstress
{
    partial class paramseditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(paramseditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.tsbSQLImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsbParamsBuild = new System.Windows.Forms.ToolStripButton();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.edSql = new ScintillaNET.Scintilla();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.edSql);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvParams);
            this.splitContainer1.Panel2.Controls.Add(this.toolBar);
            this.splitContainer1.Size = new System.Drawing.Size(811, 492);
            this.splitContainer1.SplitterDistance = 152;
            this.splitContainer1.TabIndex = 2;
            // 
            // dgvParams
            // 
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvParams.Location = new System.Drawing.Point(0, 25);
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvParams.RowTemplate.Height = 21;
            this.dgvParams.Size = new System.Drawing.Size(811, 311);
            this.dgvParams.TabIndex = 12;
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSQLImport,
            this.toolStripSeparator,
            this.tsbParamsBuild,
            this.toolStripSeparator1,
            this.tsbClear,
            this.tsbHelp});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(811, 25);
            this.toolBar.TabIndex = 11;
            // 
            // tsbSQLImport
            // 
            this.tsbSQLImport.Enabled = false;
            this.tsbSQLImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbSQLImport.Image")));
            this.tsbSQLImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSQLImport.Name = "tsbSQLImport";
            this.tsbSQLImport.Size = new System.Drawing.Size(70, 22);
            this.tsbSQLImport.Text = "导入(&O)";
            this.tsbSQLImport.Click += new System.EventHandler(this.tsbSQLImport_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbParamsBuild
            // 
            this.tsbParamsBuild.Image = ((System.Drawing.Image)(resources.GetObject("tsbParamsBuild.Image")));
            this.tsbParamsBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbParamsBuild.Name = "tsbParamsBuild";
            this.tsbParamsBuild.Size = new System.Drawing.Size(76, 22);
            this.tsbParamsBuild.Text = "批量生成";
            this.tsbParamsBuild.Click += new System.EventHandler(this.tsbParamsBuild_Click);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 22);
            this.tsbHelp.Text = "帮助(&L)";
            // 
            // edSql
            // 
            this.edSql.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edSql.Lexer = ScintillaNET.Lexer.Sql;
            this.edSql.Location = new System.Drawing.Point(0, 0);
            this.edSql.Name = "edSql";
            this.edSql.Size = new System.Drawing.Size(811, 152);
            this.edSql.TabIndex = 12;
            this.edSql.Text = "scintilla1";
            // 
            // tsbClear
            // 
            this.tsbClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbClear.Image")));
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(52, 22);
            this.tsbClear.Text = "清空";
            this.tsbClear.Click += new System.EventHandler(this.tsbClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // paramseditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 492);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "paramseditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ParamText Property";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.paramseditor_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ScintillaNET.Scintilla edSql;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton tsbSQLImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.ToolStripButton tsbParamsBuild;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbClear;
    }
}