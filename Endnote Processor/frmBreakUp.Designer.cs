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
            this.pnlHowMany = new System.Windows.Forms.Panel();
            this.cbxHowMany = new System.Windows.Forms.ComboBox();
            this.lblHowMany = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlSplit = new System.Windows.Forms.Panel();
            this.txtSplit = new System.Windows.Forms.TextBox();
            this.lblSplit = new System.Windows.Forms.Label();
            this.pnlHowMany.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHowMany
            // 
            this.pnlHowMany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHowMany.Controls.Add(this.cbxHowMany);
            this.pnlHowMany.Controls.Add(this.lblHowMany);
            this.pnlHowMany.Location = new System.Drawing.Point(2, 11);
            this.pnlHowMany.Name = "pnlHowMany";
            this.pnlHowMany.Size = new System.Drawing.Size(280, 196);
            this.pnlHowMany.TabIndex = 0;
            // 
            // cbxHowMany
            // 
            this.cbxHowMany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxHowMany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cbxHowMany.Location = new System.Drawing.Point(80, 65);
            this.cbxHowMany.Name = "cbxHowMany";
            this.cbxHowMany.Size = new System.Drawing.Size(121, 21);
            this.cbxHowMany.TabIndex = 1;
            // 
            // lblHowMany
            // 
            this.lblHowMany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHowMany.Location = new System.Drawing.Point(48, 28);
            this.lblHowMany.Name = "lblHowMany";
            this.lblHowMany.Size = new System.Drawing.Size(186, 37);
            this.lblHowMany.TabIndex = 0;
            this.lblHowMany.Text = "How many endnotes would you like to split the base into?";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(206, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(123, 212);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlSplit
            // 
            this.pnlSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSplit.Controls.Add(this.txtSplit);
            this.pnlSplit.Controls.Add(this.lblSplit);
            this.pnlSplit.Location = new System.Drawing.Point(2, 3);
            this.pnlSplit.Name = "pnlSplit";
            this.pnlSplit.Size = new System.Drawing.Size(280, 196);
            this.pnlSplit.TabIndex = 3;
            this.pnlSplit.Visible = false;
            // 
            // txtSplit
            // 
            this.txtSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSplit.Location = new System.Drawing.Point(6, 49);
            this.txtSplit.Multiline = true;
            this.txtSplit.Name = "txtSplit";
            this.txtSplit.ReadOnly = true;
            this.txtSplit.Size = new System.Drawing.Size(268, 133);
            this.txtSplit.TabIndex = 1;
            this.txtSplit.Click += new System.EventHandler(this.txtSplit_Click);
            // 
            // lblSplit
            // 
            this.lblSplit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSplit.Location = new System.Drawing.Point(5, 3);
            this.lblSplit.Name = "lblSplit";
            this.lblSplit.Size = new System.Drawing.Size(269, 40);
            this.lblSplit.TabIndex = 0;
            this.lblSplit.Text = "Click where you would like the first endnote to end.  The system will highlight f" +
    "rom the beginning to the point of your click.";
            // 
            // frmBreakUp
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(285, 240);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlSplit);
            this.Controls.Add(this.pnlHowMany);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(296, 270);
            this.Name = "frmBreakUp";
            this.Text = "Break Up a Single Endnote";
            this.Load += new System.EventHandler(this.frmBreakUp_Load);
            this.pnlHowMany.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            this.pnlSplit.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlHowMany;
        private System.Windows.Forms.Panel pnlSplit;
        private System.Windows.Forms.Label lblHowMany;
        private System.Windows.Forms.Label lblSplit;
        private System.Windows.Forms.TextBox txtSplit;
        private System.Windows.Forms.ComboBox cbxHowMany;
    }
}
