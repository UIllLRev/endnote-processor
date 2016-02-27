namespace FirstVistaTest
{
    partial class frmBreakUp
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
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnOK = new System.Windows.Forms.Button();
            this.pnlHowMany = new System.Windows.Forms.Panel();
            this.cbxHowMany = new System.Windows.Forms.ComboBox();
            this.lblHowMany = new System.Windows.Forms.Label();
            this.pnlHowMany.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btnCancel
            // 
            this._btnCancel.Location = new System.Drawing.Point(176, 182);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 0;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // _btnOK
            // 
            this._btnOK.Location = new System.Drawing.Point(21, 182);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 23);
            this._btnOK.TabIndex = 1;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
            // 
            // pnlHowMany
            // 
            this.pnlHowMany.Controls.Add(this.lblHowMany);
            this.pnlHowMany.Controls.Add(this.cbxHowMany);
            this.pnlHowMany.Location = new System.Drawing.Point(21, 31);
            this.pnlHowMany.Name = "pnlHowMany";
            this.pnlHowMany.Size = new System.Drawing.Size(200, 100);
            this.pnlHowMany.TabIndex = 2;
            // 
            // cbxHowMany
            // 
            this.cbxHowMany.FormattingEnabled = true;
            this.cbxHowMany.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbxHowMany.Location = new System.Drawing.Point(76, 34);
            this.cbxHowMany.Name = "cbxHowMany";
            this.cbxHowMany.Size = new System.Drawing.Size(121, 21);
            this.cbxHowMany.TabIndex = 0;
            // 
            // lblHowMany
            // 
            this.lblHowMany.AutoSize = true;
            this.lblHowMany.Location = new System.Drawing.Point(4, 41);
            this.lblHowMany.Name = "lblHowMany";
            this.lblHowMany.Size = new System.Drawing.Size(57, 13);
            this.lblHowMany.TabIndex = 1;
            this.lblHowMany.Text = "How many";
            // 
            // frmBreakUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pnlHowMany);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._btnCancel);
            this.Name = "frmBreakUp";
            this.Text = "frmBreakUp";
            this.Load += new System.EventHandler(this.frmBreakUp_Load);
            this.pnlHowMany.ResumeLayout(false);
            this.pnlHowMany.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Panel pnlHowMany;
        private System.Windows.Forms.Label lblHowMany;
        private System.Windows.Forms.ComboBox cbxHowMany;
    }
}