using System;
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
        string sDelimiter = "|*#*|";
        string sDelimiter2 = "|*&*|";
        string sDelimiter3 = "|*$*|";
        string sDelimiter4 = "|*@*|";
        string sDelimiter5 = "|*%*|";
        string sDelimiter6 = "|*!*|";
        bool bSaved = true;
        bool bExitGenerated = false;
        bool bSavedProgress = true;
        bool isUpdate = false;
        int oldSelectedIndex = 0;

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
    }
}
