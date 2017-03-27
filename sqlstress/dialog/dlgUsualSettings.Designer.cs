namespace sqlstress.dialog
{
    partial class dlgUsualSettings
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
            this.pnlclient = new System.Windows.Forms.Panel();
            this.btSave = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.listSettings = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlclient.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlclient
            // 
            this.pnlclient.Controls.Add(this.btSave);
            this.pnlclient.Controls.Add(this.btDelete);
            this.pnlclient.Controls.Add(this.btAdd);
            this.pnlclient.Controls.Add(this.listSettings);
            this.pnlclient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlclient.Location = new System.Drawing.Point(0, 0);
            this.pnlclient.Margin = new System.Windows.Forms.Padding(8);
            this.pnlclient.Name = "pnlclient";
            this.pnlclient.Size = new System.Drawing.Size(406, 269);
            this.pnlclient.TabIndex = 0;
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Location = new System.Drawing.Point(304, 235);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(91, 26);
            this.btSave.TabIndex = 3;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btDelete.Location = new System.Drawing.Point(112, 235);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(91, 26);
            this.btDelete.TabIndex = 2;
            this.btDelete.Text = "删除";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAdd.Location = new System.Drawing.Point(11, 235);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(91, 26);
            this.btAdd.TabIndex = 1;
            this.btAdd.Text = "增加";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // listSettings
            // 
            this.listSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listSettings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listSettings.FullRowSelect = true;
            this.listSettings.GridLines = true;
            this.listSettings.Location = new System.Drawing.Point(12, 12);
            this.listSettings.MultiSelect = false;
            this.listSettings.Name = "listSettings";
            this.listSettings.Size = new System.Drawing.Size(383, 217);
            this.listSettings.TabIndex = 0;
            this.listSettings.UseCompatibleStateImageBehavior = false;
            this.listSettings.View = System.Windows.Forms.View.Details;
            this.listSettings.DoubleClick += new System.EventHandler(this.listSettings_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 114;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类别";
            this.columnHeader2.Width = 65;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "参数";
            this.columnHeader3.Width = 169;
            // 
            // dlgUsualSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 269);
            this.Controls.Add(this.pnlclient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "dlgUsualSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UsualSettings";
            this.pnlclient.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlclient;
        private System.Windows.Forms.ListView listSettings;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btSave;
    }
}