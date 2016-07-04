using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FirstVistaTest
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
			base.add_Load(new EventHandler(this.frmBreakUp_Load));
			this.iPanelCnt = 0;
			this.bCancel = true;
			this.iDivCnt = 1;
			this.iCnt = 0;
			this.bNextClick = false;
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmBreakUp_Load(object sender, EventArgs e)
		{
			this.cbxHowMany.set_SelectedIndex(0);
			this.pnlSplit.set_Visible(false);
			this.pnlSplit.SendToBack();
			this.pnlHowMany.set_Visible(true);
			this.pnlHowMany.BringToFront();
			this.btnOK.set_Text("Next");
			this.txtSplit.set_Text(StringType.FromObject(this.parentfrm.sEndNoteArray.get_Item(this.iIndex)));
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
					this.iDivCnt = int.Parse(StringType.FromObject(this.cbxHowMany.get_SelectedItem()));
					this.pnlHowMany.set_Visible(false);
					this.pnlHowMany.SendToBack();
					this.pnlSplit.set_Visible(true);
					this.pnlSplit.BringToFront();
					this.iPanelCnt++;
					if (this.iDivCnt == 2)
					{
						this.btnOK.set_Text("OK");
					}
					this.set_Text("Break " + StringType.FromInteger(this.iPanelCnt) + " of " + StringType.FromInteger(this.iDivCnt));
				}
				else if (this.iPanelCnt > 0)
				{
					switch (this.iPanelCnt)
					{
					case 1:
						this.lblSplit.set_Text("Click where you would like the second endnote to end.  The system will highlight from the beginning to the point of your click.");
						break;
					case 2:
						this.lblSplit.set_Text("Click where you would like the third endnote to end.  The system will highlight from the beginning to the point of your click.");
						break;
					case 3:
						this.lblSplit.set_Text("Click where you would like the fourth endnote to end.  The system will highlight from the beginning to the point of your click.");
						break;
					case 4:
						this.lblSplit.set_Text("Click where you would like the fifth endnote to end.  The system will highlight from the beginning to the point of your click.");
						break;
					case 5:
						this.lblSplit.set_Text("Click where you would like the sixth endnote to end.  The system will highlight from the beginning to the point of your click.");
						break;
					case 6:
						this.lblSplit.set_Text("Click where you would like the seventh endnote to end.  The system will highlight from the beginning to the point of your click.");
						break;
					case 7:
						this.lblSplit.set_Text("Click where you would like the eighth endnote to end.  The system will highlight from the beginning to the point of your click.");
						break;
					case 8:
						this.lblSplit.set_Text("Click where you would like the ninth endnote to end.  The system will highlight from the beginning to the point of your click.");
						break;
					}
					this.parentfrm.sEndNoteArray.set_Item(this.iIndex, this.txtSplit.get_Text().Substring(0, this.iCnt).Trim());
					this.parentfrm.sEndNoteArray.Insert(this.iIndex + 1, this.txtSplit.get_Text().Substring(this.iCnt).Trim());
					NoteInfo noteInfo = (NoteInfo)this.parentfrm.sEndNoteInfo.get_Item(this.iIndex);
					this.parentfrm.sEndNoteInfo.Insert(this.iIndex + 1, noteInfo.copy());
					this.txtSplit.set_Text(this.txtSplit.get_Text().Substring(this.iCnt).Trim());
					this.iIndex++;
					if (this.iPanelCnt == this.iDivCnt - 2)
					{
						this.btnOK.set_Text("OK");
					}
					else if (this.iPanelCnt == this.iDivCnt - 1)
					{
						this.bCancel = true;
						this.Close();
					}
					this.iPanelCnt++;
					this.set_Text("Break " + StringType.FromInteger(this.iPanelCnt) + " of " + StringType.FromInteger(this.iDivCnt));
					this.btnCancel.set_Text("Close");
				}
				this.bNextClick = false;
			}
		}

		private void txtSplit_Click(object sender, EventArgs e)
		{
			if (!this.bNextClick)
			{
				this.iCnt = this.txtSplit.get_SelectionStart();
				this.txtSplit.Select(0, this.txtSplit.get_SelectionStart());
			}
		}
	}
}
