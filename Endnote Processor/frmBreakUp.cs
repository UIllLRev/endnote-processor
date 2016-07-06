using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace EndnoteProcessor
{
    public partial class frmBreakUp : Form
    {
        private int iPanelCnt;

        private bool bCancel;

        public ProcessingForm parentfrm;

        public int iIndex;

        private int iDivCnt;

        private int iCnt;

        private bool bNextClick;

        public frmBreakUp()
        {
            this.iPanelCnt = 0;
            this.bCancel = true;
            this.iDivCnt = 1;
            this.iCnt = 0;
            this.bNextClick = false;
            this.InitializeComponent();
        }

        private void frmBreakUp_Load(object sender, EventArgs e)
        {
            this.cbxHowMany.SelectedIndex = 0;
            this.pnlSplit.Visible = false;
            this.pnlSplit.SendToBack();
            this.pnlHowMany.Visible = true;
            this.pnlHowMany.BringToFront();
            this.btnOK.Text = "Next";
            this.txtSplit.Text = (string)this.parentfrm.sEndNoteArray[iIndex];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.bNextClick = true;
            checked
            {
                if (this.iPanelCnt == 0)
                {
                    this.iDivCnt = int.Parse((string)this.cbxHowMany.SelectedItem);
                    this.pnlHowMany.Visible = false;
                    this.pnlHowMany.SendToBack();
                    this.pnlSplit.Visible = true;
                    this.pnlSplit.BringToFront();
                    this.iPanelCnt++;
                    if (this.iDivCnt == 2)
                    {
                        this.btnOK.Text = "OK";
                    }
                    this.Text = "Break " + this.iPanelCnt.ToString() + " of " + this.iDivCnt.ToString();
                }
                else if (this.iPanelCnt > 0)
                {
                    switch (this.iPanelCnt)
                    {
                    case 1:
                        this.lblSplit.Text = "Click where you would like the second endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 2:
                        this.lblSplit.Text = "Click where you would like the third endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 3:
                        this.lblSplit.Text = "Click where you would like the fourth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 4:
                        this.lblSplit.Text = "Click where you would like the fifth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 5:
                        this.lblSplit.Text = "Click where you would like the sixth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 6:
                        this.lblSplit.Text = "Click where you would like the seventh endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 7:
                        this.lblSplit.Text = "Click where you would like the eighth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 8:
                        this.lblSplit.Text = "Click where you would like the ninth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    }
                    this.parentfrm.sEndNoteArray[iIndex] = txtSplit.Text.Substring(0, this.iCnt).Trim();
                    this.parentfrm.sEndNoteArray.Insert(iIndex + 1, txtSplit.Text.Substring(this.iCnt).Trim());
                    NoteInfo noteInfo = (NoteInfo)parentfrm.sEndNoteInfo[iIndex];
                    parentfrm.sEndNoteInfo.Insert(iIndex + 1, noteInfo.copy());
                    txtSplit.Text = txtSplit.Text.Substring(iCnt).Trim();
                    iIndex++;
                    if (this.iPanelCnt == this.iDivCnt - 2)
                    {
                        this.btnOK.Text = "OK";
                    }
                    else if (this.iPanelCnt == this.iDivCnt - 1)
                    {
                        this.bCancel = true;
                        this.Close();
                    }
                    this.iPanelCnt++;
                    this.Text = "Break " + iPanelCnt.ToString() + " of " + this.iDivCnt.ToString();
                    this.btnCancel.Text = "Close";
                }
                this.bNextClick = false;
            }
        }

        private void txtSplit_Click(object sender, EventArgs e)
        {
            if (!this.bNextClick)
            {
                iCnt = txtSplit.SelectionStart;
                txtSplit.Select(0, txtSplit.SelectionStart);
            }
        }
    }
}
