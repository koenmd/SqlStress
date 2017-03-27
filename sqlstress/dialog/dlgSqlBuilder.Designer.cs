namespace sqlstress.dialog
{
    partial class dlgSqlBuilder
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.datapicker = new sqlstress.dialog.DatapickView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btPreview = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.edResult = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.datapicker);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.edResult);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(811, 492);
            this.splitContainer1.SplitterDistance = 366;
            this.splitContainer1.TabIndex = 0;
            // 
            // datapicker
            // 
            this.datapicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datapicker.Location = new System.Drawing.Point(0, 0);
            this.datapicker.Name = "datapicker";
            this.datapicker.Size = new System.Drawing.Size(811, 366);
            this.datapicker.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btPreview);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(811, 36);
            this.panel1.TabIndex = 1;
            // 
            // btPreview
            // 
            this.btPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.btPreview.Location = new System.Drawing.Point(651, 0);
            this.btPreview.Name = "btPreview";
            this.btPreview.Size = new System.Drawing.Size(80, 36);
            this.btPreview.TabIndex = 0;
            this.btPreview.Text = "预览";
            this.btPreview.UseVisualStyleBackColor = true;
            this.btPreview.Click += new System.EventHandler(this.btPreview_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btCancel.Location = new System.Drawing.Point(0, 0);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(80, 36);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btOK.Location = new System.Drawing.Point(731, 0);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(80, 36);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // edResult
            // 
            this.edResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edResult.Font = new System.Drawing.Font("Cordia New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edResult.Location = new System.Drawing.Point(0, 0);
            this.edResult.Multiline = true;
            this.edResult.Name = "edResult";
            this.edResult.Size = new System.Drawing.Size(811, 86);
            this.edResult.TabIndex = 3;
            // 
            // dlgSqlBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 492);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "dlgSqlBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SqlBuilder";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DatapickView datapicker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btPreview;
        private System.Windows.Forms.TextBox edResult;
    }
}