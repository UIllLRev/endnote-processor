namespace FirstVistaTest
{
    partial class frmProgress
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
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(21, 12);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(191, 13);
            this.prgBar.TabIndex = 0;
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.Location = new System.Drawing.Point(69, 32);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(68, 13);
            this.lblProcessing.TabIndex = 1;
            this.lblProcessing.Text = "Processing...";
            // 
            // frmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblProcessing);
            this.Controls.Add(this.prgBar);
            this.Name = "frmProgress";
            this.Text = "Processing Endnotes";
            this.Load += new System.EventHandler(this.frmProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Label lblProcessing;
    }
}