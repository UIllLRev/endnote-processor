namespace FirstVistaTest
{
    partial class ProcessingForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.txtENText = new System.Windows.Forms.TextBox();
            this.lstNotes = new System.Windows.Forms.ListBox();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSVsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.openPartialEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProgressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(484, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // txtENText
            // 
            this.txtENText.Location = new System.Drawing.Point(184, 16);
            this.txtENText.Multiline = true;
            this.txtENText.Name = "txtENText";
            this.txtENText.Size = new System.Drawing.Size(200, 256);
            this.txtENText.TabIndex = 1;
            // 
            // lstNotes
            // 
            this.lstNotes.FormattingEnabled = true;
            this.lstNotes.IntegralHeight = false;
            this.lstNotes.Location = new System.Drawing.Point(2, 16);
            this.lstNotes.Name = "lstNotes";
            this.lstNotes.Size = new System.Drawing.Size(176, 320);
            this.lstNotes.TabIndex = 2;
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exportCSVsToolStripMenuItem
            // 
            this.exportCSVsToolStripMenuItem.Name = "exportCSVsToolStripMenuItem";
            this.exportCSVsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exportCSVsToolStripMenuItem.Text = "Export CSVs";
            this.exportCSVsToolStripMenuItem.Click += new System.EventHandler(this.exportCSVsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 6);
            // 
            // openPartialEditToolStripMenuItem
            // 
            this.openPartialEditToolStripMenuItem.Name = "openPartialEditToolStripMenuItem";
            this.openPartialEditToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openPartialEditToolStripMenuItem.Text = "Open Partial Edit...";
            this.openPartialEditToolStripMenuItem.Click += new System.EventHandler(this.openPartialEditToolStripMenuItem_Click);
            // 
            // saveProgressToolStripMenuItem
            // 
            this.saveProgressToolStripMenuItem.Name = "saveProgressToolStripMenuItem";
            this.saveProgressToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveProgressToolStripMenuItem.Text = "Save Progress...";
            this.saveProgressToolStripMenuItem.Click += new System.EventHandler(this.saveProgressToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(168, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exportCSVsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openPartialEditToolStripMenuItem,
            this.saveProgressToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // ProcessingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.lstNotes);
            this.Controls.Add(this.txtENText);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProcessingForm";
            this.Text = "ProcessingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessingForm_FormClosing);
            this.Load += new System.EventHandler(this.ProcessingForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProcessingForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox txtENText;
        private System.Windows.Forms.ListBox lstNotes;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSVsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openPartialEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProgressToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}