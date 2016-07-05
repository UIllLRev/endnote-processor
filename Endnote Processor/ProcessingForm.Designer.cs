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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessingForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnOpenPart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSaveProg = new System.Windows.Forms.ToolStripMenuItem();
            this.mnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.txtENText = new System.Windows.Forms.TextBox();
            this.lblENText = new System.Windows.Forms.Label();
            this.lstNotes = new System.Windows.Forms.ListBox();
            this.chkSupra = new System.Windows.Forms.CheckBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnBreak = new System.Windows.Forms.Button();
            this.gbxType = new System.Windows.Forms.GroupBox();
            this.rbMiscellaneous = new System.Windows.Forms.RadioButton();
            this.rbPeriodical = new System.Windows.Forms.RadioButton();
            this.rbLegislative = new System.Windows.Forms.RadioButton();
            this.rbCase = new System.Windows.Forms.RadioButton();
            this.rbBooks = new System.Windows.Forms.RadioButton();
            this.rbJournal = new System.Windows.Forms.RadioButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.gbxType.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(520, 24);
            this.menuStrip1.TabIndex = 0;
            // 
            // mnFile
            // 
            this.mnFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnOpen,
            this.mnExport,
            this.mnClose,
            this.toolStripMenuItem1,
            this.mnOpenPart,
            this.mnSaveProg,
            this.toolStripMenuItem2,
            this.mnExit});
            this.mnFile.Name = "mnFile";
            this.mnFile.Size = new System.Drawing.Size(37, 20);
            this.mnFile.Text = "&File";
            // 
            // mnOpen
            // 
            this.mnOpen.Name = "mnOpen";
            this.mnOpen.ShortcutKeyDisplayString = "Ctrl+O";
            this.mnOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnOpen.Size = new System.Drawing.Size(246, 22);
            this.mnOpen.Text = "&Open Word Document...";
            this.mnOpen.Click += new System.EventHandler(this.mnOpen_Click);
            // 
            // mnExport
            // 
            this.mnExport.Enabled = false;
            this.mnExport.Name = "mnExport";
            this.mnExport.Size = new System.Drawing.Size(246, 22);
            this.mnExport.Text = "&Export CSVs...";
            this.mnExport.Click += new System.EventHandler(this.mnExport_Click);
            // 
            // mnClose
            // 
            this.mnClose.Enabled = false;
            this.mnClose.Name = "mnClose";
            this.mnClose.Size = new System.Drawing.Size(246, 22);
            this.mnClose.Text = "&Close Document";
            this.mnClose.Click += new System.EventHandler(this.mnClose_Click);
            // 
            // mnOpenPart
            // 
            this.mnOpenPart.Name = "mnOpenPart";
            this.mnOpenPart.Size = new System.Drawing.Size(246, 22);
            this.mnOpenPart.Text = "Open &Partial Edit...";
            this.mnOpenPart.Click += new System.EventHandler(this.mnOpenPart_Click);
            // 
            // mnSaveProg
            // 
            this.mnSaveProg.Enabled = false;
            this.mnSaveProg.Name = "mnSaveProg";
            this.mnSaveProg.Size = new System.Drawing.Size(246, 22);
            this.mnSaveProg.Text = "&Save Progress...";
            this.mnSaveProg.Click += new System.EventHandler(this.mnSaveProg_Click);
            // 
            // mnExit
            // 
            this.mnExit.Name = "mnExit";
            this.mnExit.ShortcutKeyDisplayString = "Alt+F4";
            this.mnExit.Size = new System.Drawing.Size(246, 22);
            this.mnExit.Text = "E&xit";
            this.mnExit.Click += new System.EventHandler(this.mnExit_Click);
            // 
            // txtENText
            // 
            this.txtENText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtENText.Enabled = false;
            this.txtENText.Location = new System.Drawing.Point(184, 40);
            this.txtENText.Multiline = true;
            this.txtENText.Name = "txtENText";
            this.txtENText.Size = new System.Drawing.Size(200, 256);
            this.txtENText.TabIndex = 1;
            this.txtENText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtENText_KeyDown);
            // 
            // lblENText
            // 
            this.lblENText.Location = new System.Drawing.Point(184, 24);
            this.lblENText.Name = "lblENText";
            this.lblENText.Size = new System.Drawing.Size(100, 23);
            this.lblENText.TabIndex = 2;
            this.lblENText.Text = "Endnote Text";
            // 
            // lstNotes
            // 
            this.lstNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstNotes.IntegralHeight = false;
            this.lstNotes.Location = new System.Drawing.Point(0, 24);
            this.lstNotes.Name = "lstNotes";
            this.lstNotes.Size = new System.Drawing.Size(176, 320);
            this.lstNotes.TabIndex = 3;
            this.lstNotes.SelectedIndexChanged += new System.EventHandler(this.lstNotes_SelectedIndexChanged);
            // 
            // chkSupra
            // 
            this.chkSupra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSupra.Enabled = false;
            this.chkSupra.Location = new System.Drawing.Point(392, 48);
            this.chkSupra.Name = "chkSupra";
            this.chkSupra.Size = new System.Drawing.Size(120, 40);
            this.chkSupra.TabIndex = 4;
            this.chkSupra.Text = "&Exclude from Exports";
            this.chkSupra.CheckStateChanged += new System.EventHandler(this.chkSupra_CheckStateChanged);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(437, 302);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "&Next ->";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrev.Enabled = false;
            this.btnPrev.Location = new System.Drawing.Point(360, 302);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 6;
            this.btnPrev.Text = "<- Pre&vious";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnBreak
            // 
            this.btnBreak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBreak.Enabled = false;
            this.btnBreak.Location = new System.Drawing.Point(209, 302);
            this.btnBreak.Name = "btnBreak";
            this.btnBreak.Size = new System.Drawing.Size(75, 23);
            this.btnBreak.TabIndex = 7;
            this.btnBreak.Text = "Break &Up";
            this.btnBreak.Click += new System.EventHandler(this.btnBreak_Click);
            // 
            // gbxType
            // 
            this.gbxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxType.Controls.Add(this.rbMiscellaneous);
            this.gbxType.Controls.Add(this.rbPeriodical);
            this.gbxType.Controls.Add(this.rbLegislative);
            this.gbxType.Controls.Add(this.rbCase);
            this.gbxType.Controls.Add(this.rbBooks);
            this.gbxType.Controls.Add(this.rbJournal);
            this.gbxType.Enabled = false;
            this.gbxType.Location = new System.Drawing.Point(392, 96);
            this.gbxType.Name = "gbxType";
            this.gbxType.Size = new System.Drawing.Size(120, 160);
            this.gbxType.TabIndex = 8;
            this.gbxType.TabStop = false;
            this.gbxType.Text = "Type of Reference";
            // 
            // rbMiscellaneous
            // 
            this.rbMiscellaneous.Location = new System.Drawing.Point(8, 136);
            this.rbMiscellaneous.Name = "rbMiscellaneous";
            this.rbMiscellaneous.Size = new System.Drawing.Size(96, 16);
            this.rbMiscellaneous.TabIndex = 5;
            this.rbMiscellaneous.Text = "&Miscellaneous";
            // 
            // rbPeriodical
            // 
            this.rbPeriodical.Location = new System.Drawing.Point(8, 112);
            this.rbPeriodical.Name = "rbPeriodical";
            this.rbPeriodical.Size = new System.Drawing.Size(96, 16);
            this.rbPeriodical.TabIndex = 4;
            this.rbPeriodical.Text = "&Periodical";
            // 
            // rbLegislative
            // 
            this.rbLegislative.Location = new System.Drawing.Point(8, 88);
            this.rbLegislative.Name = "rbLegislative";
            this.rbLegislative.Size = new System.Drawing.Size(96, 16);
            this.rbLegislative.TabIndex = 3;
            this.rbLegislative.Text = "&Legislative";
            // 
            // rbCase
            // 
            this.rbCase.Location = new System.Drawing.Point(8, 64);
            this.rbCase.Name = "rbCase";
            this.rbCase.Size = new System.Drawing.Size(96, 16);
            this.rbCase.TabIndex = 2;
            this.rbCase.Text = "&Case";
            // 
            // rbBooks
            // 
            this.rbBooks.Location = new System.Drawing.Point(8, 40);
            this.rbBooks.Name = "rbBooks";
            this.rbBooks.Size = new System.Drawing.Size(96, 16);
            this.rbBooks.TabIndex = 1;
            this.rbBooks.Text = "&Book";
            // 
            // rbJournal
            // 
            this.rbJournal.Location = new System.Drawing.Point(8, 16);
            this.rbJournal.Name = "rbJournal";
            this.rbJournal.Size = new System.Drawing.Size(96, 16);
            this.rbJournal.TabIndex = 0;
            this.rbJournal.Text = "&Journal";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(243, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(243, 6);
            // 
            // ProcessingForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(520, 334);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gbxType);
            this.Controls.Add(this.btnBreak);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.chkSupra);
            this.Controls.Add(this.lstNotes);
            this.Controls.Add(this.txtENText);
            this.Controls.Add(this.lblENText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(536, 370);
            this.Name = "ProcessingForm";
            this.Text = "Processing Endnotes";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ProcessingForm_Closing);
            this.Load += new System.EventHandler(this.ProcessingForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbxType.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label lblENText;
        private System.Windows.Forms.TextBox txtENText;
        private System.Windows.Forms.ListBox lstNotes;
        private System.Windows.Forms.CheckBox chkSupra;
        private System.Windows.Forms.RadioButton rbBooks;
        private System.Windows.Forms.RadioButton rbJournal;
        private System.Windows.Forms.RadioButton rbCase;
        private System.Windows.Forms.RadioButton rbLegislative;
        private System.Windows.Forms.RadioButton rbPeriodical;
        private System.Windows.Forms.RadioButton rbMiscellaneous;
        private System.Windows.Forms.Button btnBreak;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.GroupBox gbxType;
        private System.Windows.Forms.ToolStripMenuItem mnFile;
        private System.Windows.Forms.ToolStripMenuItem mnOpen;
        private System.Windows.Forms.ToolStripMenuItem mnExport;
        private System.Windows.Forms.ToolStripMenuItem mnOpenPart;
        private System.Windows.Forms.ToolStripMenuItem mnSaveProg;
        private System.Windows.Forms.ToolStripMenuItem mnExit;
        private System.Windows.Forms.ToolStripMenuItem mnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}
