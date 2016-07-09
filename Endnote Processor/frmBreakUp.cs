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

        public ProcessingForm parentfrm;

        public int iIndex;

        private int iDivCnt;

        private int iCnt;

        private bool bNextClick;

        public frmBreakUp()
        {
            iPanelCnt = 0;
            iDivCnt = 1;
            iCnt = 0;
            bNextClick = false;
            InitializeComponent();
        }

        private void frmBreakUp_Load(object sender, EventArgs e)
        {
            cbxHowMany.SelectedIndex = 0;
            pnlSplit.Visible = false;
            pnlSplit.SendToBack();
            pnlHowMany.Visible = true;
            pnlHowMany.BringToFront();
            btnOK.Text = "Next";
            txtSplit.Text = (string)parentfrm.sEndNoteArray[iIndex];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bNextClick = true;
            checked
            {
                if (iPanelCnt == 0)
                {
                    iDivCnt = int.Parse((string)cbxHowMany.SelectedItem);
                    pnlHowMany.Visible = false;
                    pnlHowMany.SendToBack();
                    pnlSplit.Visible = true;
                    pnlSplit.BringToFront();
                    iPanelCnt++;
                    if (iDivCnt == 2)
                    {
                        btnOK.Text = "OK";
                    }
                    Text = "Break " + iPanelCnt.ToString() + " of " + iDivCnt.ToString();
                }
                else if (iPanelCnt > 0)
                {
                    switch (iPanelCnt)
                    {
                    case 1:
                        lblSplit.Text = "Click where you would like the second endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 2:
                        lblSplit.Text = "Click where you would like the third endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 3:
                        lblSplit.Text = "Click where you would like the fourth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 4:
                        lblSplit.Text = "Click where you would like the fifth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 5:
                        lblSplit.Text = "Click where you would like the sixth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 6:
                        lblSplit.Text = "Click where you would like the seventh endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 7:
                        lblSplit.Text = "Click where you would like the eighth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    case 8:
                        lblSplit.Text = "Click where you would like the ninth endnote to end.  The system will highlight from the beginning to the point of your click.";
                        break;
                    }
                    parentfrm.sEndNoteArray[iIndex] = txtSplit.Text.Substring(0, iCnt).Trim();
                    parentfrm.sEndNoteArray.Insert(iIndex + 1, txtSplit.Text.Substring(iCnt).Trim());
                    NoteInfo noteInfo = (NoteInfo)parentfrm.sEndNoteInfo[iIndex];
                    parentfrm.sEndNoteInfo.Insert(iIndex + 1, noteInfo.copy());
                    txtSplit.Text = txtSplit.Text.Substring(iCnt).Trim();
                    iIndex++;
                    if (iPanelCnt == iDivCnt - 2)
                    {
                        btnOK.Text = "OK";
                    }
                    else if (iPanelCnt == iDivCnt - 1)
                    {
                        
                        Close();
                    }
                    iPanelCnt++;
                    Text = "Break " + iPanelCnt.ToString() + " of " + iDivCnt.ToString();
                    btnCancel.Text = "Close";
                }
                bNextClick = false;
            }
        }

        private void txtSplit_Click(object sender, EventArgs e)
        {
            if (!bNextClick)
            {
                iCnt = txtSplit.SelectionStart;
                txtSplit.Select(0, txtSplit.SelectionStart);
            }
        }
    }
}
