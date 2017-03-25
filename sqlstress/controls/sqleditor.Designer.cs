namespace sqlstress
{
    partial class sqleditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sqleditor));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.tsbSQLImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSQLBuild = new System.Windows.Forms.ToolStripButton();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.edSql = new ScintillaNET.Scintilla();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSQLImport,
            this.toolStripSeparator,
            this.tsbSQLBuild,
            this.tsbHelp});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(811, 25);
            this.toolBar.TabIndex = 10;
            // 
            // tsbSQLImport
            // 
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
            // tsbSQLBuild
            // 
            this.tsbSQLBuild.Image = ((System.Drawing.Image)(resources.GetObject("tsbSQLBuild.Image")));
            this.tsbSQLBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSQLBuild.Name = "tsbSQLBuild";
            this.tsbSQLBuild.Size = new System.Drawing.Size(76, 22);
            this.tsbSQLBuild.Text = "批量生成";
            this.tsbSQLBuild.ToolTipText = "tsbSQLBuild";
            this.tsbSQLBuild.Click += new System.EventHandler(this.tsbSQLBuild_Click);
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
            this.edSql.Location = new System.Drawing.Point(0, 25);
            this.edSql.Name = "edSql";
            this.edSql.Size = new System.Drawing.Size(811, 467);
            this.edSql.TabIndex = 11;
            this.edSql.Text = "scintilla1";
            // 
            // sqleditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 492);
            this.Controls.Add(this.edSql);
            this.Controls.Add(this.toolBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "sqleditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SqlText Property";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.sqleditor_FormClosed);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton tsbSQLImport;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private ScintillaNET.Scintilla edSql;
        private System.Windows.Forms.ToolStripButton tsbSQLBuild;
    }
}