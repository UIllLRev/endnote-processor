using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FirstVistaTest
{
    public partial class frmProgress : Form
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            prgBar.Minimum = 0;
            prgBar.Maximum = 100;
            prgBar.Step = 1;
            prgBar.Value = 0;
        }

        public void ResetBar()
        {
            prgBar.Value = 0;
            Application.DoEvents();
        }

        public void SetMaxVal(int max)
        {
            prgBar.Maximum = max;
            Application.DoEvents();
        }

        public void SetMinVal(int min)
        {
            prgBar.Minimum = min;
            Application.DoEvents();
        }

        public void StepUp(int stepSize)
        {
            prgBar.Value = prgBar.Value + stepSize;
            Application.DoEvents();
        }
    }
}
