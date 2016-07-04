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
            base.add_Load(new EventHandler(this.frmProgress_Load));
            this.InitializeComponent();
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            this.prgBar.set_Minimum(0);
            this.prgBar.set_Maximum(100);
            this.prgBar.set_Step(1);
            this.prgBar.set_Value(0);
        }

        public void ResetBar()
        {
            this.prgBar.set_Value(0);
            Application.DoEvents();
        }

        public void SetMaxVal(int max)
        {
            this.prgBar.set_Maximum(max);
            Application.DoEvents();
        }

        public void SetMinVal(int min)
        {
            this.prgBar.set_Minimum(min);
            Application.DoEvents();
        }

        public void StepUp(int stepSize)
        {
            this.prgBar.set_Value(checked(this.prgBar.get_Value() + stepSize));
            Application.DoEvents();
        }
    }
}
