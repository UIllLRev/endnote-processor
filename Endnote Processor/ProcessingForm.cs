using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FirstVistaTest
{
    public partial class ProcessingForm : Form
    {
        private Microsoft.Office.Interop.Word.Application oWordApp;
        private Microsoft.Office.Interop.Word.Document oWordDoc;
        public ArrayList sEndNoteArray;
        public ArrayList sEndNoteInfo;
        public string sDelimiter = "|*#*|";
        public string sDelimiter2 = "|*&*|";
        public string sDelimiter3 = "|*$*|";
        public string sDelimiter4 = "|*@*|";
        public string sDelimiter5 = "|*%*|";
        public string sDelimiter6 = "|*!*|";
        public bool bSaved = true;
        public bool bExitGenerated = false;
        public bool bSavedProgress = true;
        public bool isUpdate = false;
        public int oldSelectedIndex = 0;

        [STAThread]
        public static void Main()
        {
            Application.Run(new ProcessingForm());
        }

        public ProcessingForm()
        {
            InitializeComponent();            
        }

        private void ProcessingForm_Load(object s, EventArgs a)
        {
            
        }

        private void ProcessingForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ProcessingForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void openPartialToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OpenFileDialog().ShowDialog();
        }

        private void exportCSVsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openPartialEditToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
