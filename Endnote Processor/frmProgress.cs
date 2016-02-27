using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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

        public void stepUp(int stepSize)
        {
            prgBar.Value += stepSize;
            Application.DoEvents();
        }
    }
}
