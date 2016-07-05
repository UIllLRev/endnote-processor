using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;

using DocumentFormat.OpenXml.Packaging;

namespace FirstVistaTest
{
    public partial class ProcessingForm : Form
    {
        private WordprocessingDocument oWordDoc;

        public ArrayList sEndNoteArray;

        public ArrayList sEndNoteInfo;

        public string sDelimiter;

        public string sDelimiter2;

        public string sDelimiter3;

        public string sDelimiter4;

        public string sDelimiter5;

        public string sDelimiter6;

        public bool bSaved;

        public bool bExitGenerated;

        public bool bSavedProgress;

        public bool isUpdate;

        public int oldSelectedIndex;

        [STAThread]
        public static void Main()
        {
            System.Windows.Forms.Application.Run(new ProcessingForm());
        }

        public ProcessingForm()
        {
            sDelimiter = "|*#*|";
            sDelimiter2 = "|*&*|";
            sDelimiter3 = "|*$*|";
            sDelimiter4 = "|*@*|";
            sDelimiter5 = "|*%*|";
            sDelimiter6 = "|*!*|";
            bSaved = true;
            bExitGenerated = false;
            bSavedProgress = true;
            isUpdate = false;
            oldSelectedIndex = 0;
            InitializeComponent();
        }

        private void mnExit_Click(object sender, EventArgs e)
        {
            if (oldSelectedIndex >= 0 && sEndNoteArray != null && oldSelectedIndex < sEndNoteArray.Count)
            {
                sEndNoteArray[oldSelectedIndex]  = txtENText.Text;
                NoteInfo noteInfo = (NoteInfo)sEndNoteInfo[oldSelectedIndex];
                noteInfo.SupraOrId = chkSupra.Checked;
                if (rbJournal.Checked)
                {
                    noteInfo.Type = 0;
                }
                if (rbBooks.Checked)
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.Checked)
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.Checked)
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.Checked)
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.Checked)
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo[oldSelectedIndex] = noteInfo;
            }
            if (!this.bSaved & !this.bSavedProgress)
            {
                DialogResult msgBoxResult = MessageBox.Show("You have not yet exported the endnotes, would you like to before exiting?", "Processing Endnotes...", MessageBoxButtons.YesNoCancel);
                if (msgBoxResult == DialogResult.Yes)
                {
                    this.ExportCSV();
                }
                else
                {
                    if (msgBoxResult == DialogResult.Cancel)
                    {
                        return;
                    }
                    if (msgBoxResult == DialogResult.No)
                    {
                        DialogResult msgBoxResult2 = MessageBox.Show("Would you like to save your progress so that it can be resumed later?", "Processing Endnotes...", MessageBoxButtons.YesNoCancel);
                        if (msgBoxResult2 == DialogResult.Yes)
                        {
                            this.SaveProgress();
                        }
                        else if (msgBoxResult2 == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
            }

            this.bExitGenerated = true;
            this.Close();
        }

        private void mnOpen_Click(object sender, EventArgs e)
        {
            checked
            {
                try
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    openFileDialog.Filter = "Word Documents (*.doc; *.docx)|*.doc;*.docx";
                    openFileDialog.Title = "Open a Word document to process...";
                    openFileDialog.CheckFileExists = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        oWordDoc = WordprocessingDocument.Open(openFileDialog.FileName, false);
                        XElement xmlDoc = oWordDoc.MainDocumentPart.EndnotesPart.GetXDocument().Root;
                        if (xmlDoc.HasElements)
                        {
                            frmProgress frmProgress = new frmProgress();
                            frmProgress.Show();
                            frmProgress.SetMinVal(0);
                            frmProgress.SetMaxVal(xmlDoc.Elements().Count());
                            this.sEndNoteArray = new ArrayList();
                            this.sEndNoteInfo = new ArrayList();
                            int i = 0;
                            NoteInfo noteInfo = new NoteInfo();
                            foreach (XElement q in xmlDoc.Elements())
                            {
                                try
                                {
                                    if (q.Value != null && q.Value.Length > 0)
                                    {
                                        string text = q.Value.Trim();
                                        if (text[0] == '.')
                                            text = text.Substring(1).Trim();
                                        sEndNoteArray.Add(text);
                                        sEndNoteInfo.Add(new NoteInfo());
                                        if (text.IndexOf("id.", StringComparison.InvariantCultureIgnoreCase) >= 0 | text.IndexOf("supra", StringComparison.InvariantCultureIgnoreCase) >= 0 | text.IndexOf("need cite", StringComparison.InvariantCultureIgnoreCase) >= 0)
                                        {
                                            noteInfo = (NoteInfo)this.sEndNoteInfo[sEndNoteInfo.Count - 1];
                                            noteInfo.SupraOrId = true;
                                            sEndNoteInfo[sEndNoteInfo.Count - 1] =  noteInfo;
                                        }
                                        frmProgress.StepUp(1);
                                    }
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        MessageBox.Show("There was an error in processing endnote #" + (i + 1).ToString(), "Processing Endnotes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        if (oWordDoc != null)
                                        {
                                            oWordDoc.Close();
                                            oWordDoc = null;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    return;
                                }
                            }
                            oWordDoc.Close();
                            updateListBox();
                            if (frmProgress != null)
                            {
                                frmProgress.Close();
                                frmProgress = null;
                            }
                            txtENText.Text = (string)sEndNoteArray[lstNotes.SelectedIndex];
                            noteInfo = (NoteInfo)sEndNoteInfo[lstNotes.SelectedIndex];
                            chkSupra.Checked = noteInfo.SupraOrId;
                            oldSelectedIndex = lstNotes.SelectedIndex;
                            switch (noteInfo.Type)
                            {
                            case 0:
                                rbJournal.Checked = true;
                                break;
                            case 1:
                                rbBooks.Checked = true;
                                break;
                            case 2:
                                rbCase.Checked = true;
                                break;
                            case 3:
                                rbLegislative.Checked = true;
                                break;
                            case 4:
                                rbPeriodical.Checked = true;
                                break;
                            case 5:
                                rbMiscellaneous.Checked = true;
                                break;
                            }
                            txtENText.Enabled = true;
                            btnBreak.Enabled = true;
                            btnPrev.Enabled = true;
                            btnNext.Enabled = true;
                            chkSupra.Enabled = true;
                            rbBooks.Enabled = true;
                            rbJournal.Enabled = true;
                            rbCase.Enabled = true;
                            rbPeriodical.Enabled = true;
                            rbLegislative.Enabled = true;
                            rbMiscellaneous.Enabled = true;
                            gbxType.Enabled = true;
                            mnOpen.Enabled = false;
                            mnOpenPart.Enabled = false;
                            mnClose.Enabled = true;
                            mnExport.Enabled = true;
                            mnSaveProg.Enabled = true;
                            bSaved = false;
                            bSavedProgress = false;
                        }
                        else
                        {
                            MessageBox.Show("There are no endnotes in this document.", "Processing Endnotes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            oWordDoc.Close();
                            this.oWordDoc = null;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There was an error opening the file, please check the file and try again.", "Processing Endnotes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    try
                    {
                        if (this.oWordDoc != null)
                        {
                            oWordDoc.Close();
                            oWordDoc = null;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void ProcessingForm_Load(object sender, EventArgs e)
        {
            try
            {
                sEndNoteArray = new ArrayList();
                sEndNoteInfo = new ArrayList();
                mnClose.Enabled = false;
                mnOpen.Enabled = true;
                mnExport.Enabled = false;
                mnOpenPart.Enabled = true;
                mnSaveProg.Enabled = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show("You must have Microsoft Word XP or higher installed to use this program: " + exp.Message, "Processing Endnotes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void updateListBox()
        {
            isUpdate = true;
            lstNotes.BeginUpdate();
            int num = 0;
            try
            {
                num = lstNotes.SelectedIndex;
            }
            catch (Exception)
            {
            }
            if (num < 0)
            {
                num = 0;
            }
            this.lstNotes.Items.Clear();
            checked
            {
                int num2 = this.sEndNoteArray.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    NoteInfo noteInfo = (NoteInfo)sEndNoteInfo[i];
                    lstNotes.Items.Add("Endnote " + (i + 1).ToString());
                }
                this.lstNotes.EndUpdate();
                this.lstNotes.SelectedIndex = num;
                this.isUpdate = false;
            }
        }

        private void lstNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.isUpdate)
            {
                NoteInfo noteInfo;
                if (oldSelectedIndex >= 0)
                {
                    sEndNoteArray[oldSelectedIndex] = txtENText.Text;
                    noteInfo = (NoteInfo)sEndNoteInfo[oldSelectedIndex];
                    noteInfo.SupraOrId = chkSupra.Checked;
                    if (rbJournal.Checked)
                    {
                        noteInfo.Type = 0;
                    }
                    if (rbBooks.Checked)
                    {
                        noteInfo.Type = 1;
                    }
                    if (rbCase.Checked)
                    {
                        noteInfo.Type = 2;
                    }
                    if (rbLegislative.Checked)
                    {
                        noteInfo.Type = 3;
                    }
                    if (rbPeriodical.Checked)
                    {
                        noteInfo.Type = 4;
                    }
                    if (rbMiscellaneous.Checked)
                    {
                        noteInfo.Type = 5;
                    }
                    sEndNoteInfo[oldSelectedIndex] = noteInfo;
                }
                txtENText.Text = (string)sEndNoteArray[lstNotes.SelectedIndex];
                noteInfo = (NoteInfo)sEndNoteInfo[lstNotes.SelectedIndex];
                this.chkSupra.Checked = noteInfo.SupraOrId;
                switch (noteInfo.Type)
                {
                case 0:
                    rbJournal.Checked = true;
                    break;
                case 1:
                    rbBooks.Checked = true;
                    break;
                case 2:
                    rbCase.Checked = true;
                    break;
                case 3:
                    rbLegislative.Checked = true;
                    break;
                case 4:
                    rbPeriodical.Checked = true;
                    break;
                case 5:
                    rbMiscellaneous.Checked = true;
                    break;
                }
                oldSelectedIndex = this.lstNotes.SelectedIndex;
                bSaved = false;
                bSavedProgress = false;
                txtENText.Focus();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.lstNotes.SelectedIndex > 0)
            {
                ListBox lstNotes = this.lstNotes;
                lstNotes.SelectedIndex = lstNotes.SelectedIndex - 1;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            checked
            {
                if (this.lstNotes.SelectedIndex < this.lstNotes.Items.Count - 1)
                {
                    ListBox lstNotes = this.lstNotes;
                    lstNotes.SelectedIndex = lstNotes.SelectedIndex + 1;
                }
            }
        }

        private void btnBreak_Click(object sender, EventArgs e)
        {
            bSaved = false;
            bSavedProgress = false;
            NoteInfo noteInfo;
            if (oldSelectedIndex >= 0)
            {
                sEndNoteArray[oldSelectedIndex] = txtENText.Text;
                noteInfo = (NoteInfo)sEndNoteInfo[oldSelectedIndex];
                noteInfo.SupraOrId = chkSupra.Checked;
                if (rbJournal.Checked)
                {
                    noteInfo.Type = 0;
                }
                if (rbBooks.Checked)
                {
                    noteInfo.Type = 1;
                }
                if (rbCase.Checked)
                {
                    noteInfo.Type = 2;
                }
                if (rbLegislative.Checked)
                {
                    noteInfo.Type = 3;
                }
                if (rbPeriodical.Checked)
                {
                    noteInfo.Type = 4;
                }
                if (rbMiscellaneous.Checked)
                {
                    noteInfo.Type = 5;
                }
                sEndNoteInfo[oldSelectedIndex] = noteInfo;
            }
            new frmBreakUp
            {
                parentfrm = this,
                iIndex = lstNotes.SelectedIndex
            }.ShowDialog();
            updateListBox();
            txtENText.Text = (string)sEndNoteArray[lstNotes.SelectedIndex];
            noteInfo = (NoteInfo)sEndNoteInfo[lstNotes.SelectedIndex];
            chkSupra.Checked = noteInfo.SupraOrId;
            switch (noteInfo.Type)
            {
            case 0:
                this.rbJournal.Checked = true;
                break;
            case 1:
                this.rbBooks.Checked = true;
                break;
            case 2:
                this.rbCase.Checked = true;
                break;
            case 3:
                this.rbLegislative.Checked = true;
                break;
            case 4:
                this.rbPeriodical.Checked = true;
                break;
            case 5:
                this.rbMiscellaneous.Checked = true;
                break;
            }
            this.oldSelectedIndex = this.lstNotes.SelectedIndex;
        }

        private void mnExport_Click(object sender, EventArgs e)
        {
            if (this.oldSelectedIndex >= 0 && this.sEndNoteArray != null && this.oldSelectedIndex < this.sEndNoteArray.Count)
            {
                sEndNoteArray[oldSelectedIndex] = txtENText.Text;
                NoteInfo noteInfo = (NoteInfo)sEndNoteInfo[oldSelectedIndex];
                noteInfo.SupraOrId = chkSupra.Checked;
                if (this.rbJournal.Checked)
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.Checked)
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.Checked)
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.Checked)
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.Checked)
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.Checked)
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo[oldSelectedIndex] = noteInfo;
            }
            ExportCSV();
        }

        private void ExportCSV()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Save the export file collection...";
            saveFileDialog.Filter = "Directory|";
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            checked
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    frmProgress frmProgress = new frmProgress();
                    frmProgress.SetMinVal(0);
                    frmProgress.SetMaxVal(sEndNoteArray.Count);
                    if (!Directory.Exists(saveFileDialog.FileName))
                    {
                        Directory.CreateDirectory(saveFileDialog.FileName);
                    }
                    string text = saveFileDialog.FileName + "\\";
                    ArrayList arrayList = new ArrayList();
                    ArrayList arrayList2 = new ArrayList();
                    ArrayList arrayList3 = new ArrayList();
                    ArrayList arrayList4 = new ArrayList();
                    ArrayList arrayList5 = new ArrayList();
                    ArrayList arrayList6 = new ArrayList();
                    int num = this.sEndNoteArray.Count - 1;
                    for (int i = 0; i <= num; i++)
                    {
                        NoteInfo noteInfo = (NoteInfo)sEndNoteInfo[i];
                        if (!noteInfo.SupraOrId)
                        {
                            switch (noteInfo.Type)
                            {
                            case 0:
                                arrayList3.Add(sEndNoteArray[i]);
                                break;
                            case 1:
                                arrayList.Add(sEndNoteArray[i]);
                                break;
                            case 2:
                                arrayList2.Add(sEndNoteArray[i]);
                                break;
                            case 3:
                                arrayList4.Add(sEndNoteArray[i]);
                                break;
                            case 4:
                                arrayList5.Add(sEndNoteArray[i]);
                                break;
                            case 5:
                                arrayList6.Add(sEndNoteArray[i]);
                                break;
                            }
                        }
                        frmProgress.StepUp(1);
                    }
                    if (arrayList3.Count > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList3.Count);
                        StreamWriter streamWriter = new StreamWriter(text + "journals.csv", false);
                        int num2 = arrayList3.Count - 1;
                        for (int i = 0; i <= num2; i++)
                        {
                            streamWriter.Write(arrayList3[i] + sDelimiter);
                            frmProgress.StepUp(1);
                        }
                        streamWriter.Close();
                    }
                    if (arrayList.Count > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList.Count);
                        StreamWriter streamWriter2 = new StreamWriter(text + "books.csv", false);
                        int num3 = arrayList.Count - 1;
                        for (int i = 0; i <= num3; i++)
                        {
                            streamWriter2.Write(arrayList[i] + sDelimiter);
                            frmProgress.StepUp(1);
                        }
                        streamWriter2.Close();
                    }
                    if (arrayList2.Count > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList2.Count);
                        StreamWriter streamWriter3 = new StreamWriter(text + "cases.csv", false);
                        int num4 = arrayList2.Count - 1;
                        for (int i = 0; i <= num4; i++)
                        {
                            streamWriter3.Write(arrayList2[i] + sDelimiter);
                            frmProgress.StepUp(1);
                        }
                        streamWriter3.Close();
                    }
                    if (arrayList4.Count > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList4.Count);
                        StreamWriter streamWriter4 = new StreamWriter(text + "legislative.csv", false);
                        int num5 = arrayList4.Count - 1;
                        for (int i = 0; i <= num5; i++)
                        {
                            streamWriter4.Write(arrayList4[i] + sDelimiter);
                            frmProgress.StepUp(1);
                        }
                        streamWriter4.Close();
                    }
                    if (arrayList5.Count > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList5.Count);
                        StreamWriter streamWriter5 = new StreamWriter(text + "periodicals.csv", false);
                        int num6 = arrayList5.Count - 1;
                        for (int i = 0; i <= num6; i++)
                        {
                            streamWriter5.Write(arrayList5[i] + sDelimiter);
                            frmProgress.StepUp(1);
                        }
                        streamWriter5.Close();
                    }
                    if (arrayList6.Count > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList6.Count);
                        StreamWriter streamWriter6 = new StreamWriter(text + "miscellaneous.csv", false);
                        for (int i = 0; i <= arrayList6.Count - 1; i++)
                        {
                            streamWriter6.Write(arrayList6[i] + sDelimiter);
                            frmProgress.StepUp(1);
                        }
                        streamWriter6.Close();
                    }
                    this.bSaved = true;
                    this.bSavedProgress = true;
                    if (frmProgress != null)
                    {
                        frmProgress.Close();
                    }
                }
            }
        }

        private void mnClose_Click(object sender, EventArgs e)
        {
            if (this.oldSelectedIndex >= 0)
            {
                sEndNoteArray[oldSelectedIndex] = txtENText.Text;
                NoteInfo noteInfo = (NoteInfo)sEndNoteInfo[oldSelectedIndex];
                noteInfo.SupraOrId = this.chkSupra.Checked;
                if (this.rbJournal.Checked)
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.Checked)
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.Checked)
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.Checked)
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.Checked)
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.Checked)
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo[oldSelectedIndex] = noteInfo;
            }
            if (!this.bSaved & !this.bSavedProgress)
            {
                DialogResult msgBoxResult = MessageBox.Show("You have not yet exported the endnotes, would you like to before closing?", "Processing Endnotes...", MessageBoxButtons.YesNoCancel);
                if (msgBoxResult == DialogResult.Yes)
                {
                    this.ExportCSV();
                }
                else
                {
                    if (msgBoxResult == DialogResult.Cancel)
                    {
                        return;
                    }
                    if (msgBoxResult == DialogResult.No)
                    {
                        DialogResult msgBoxResult2 = MessageBox.Show("Would you like to save your progress so that it can be resumed later?", "Processing Endnotes...", MessageBoxButtons.YesNoCancel);
                        if (msgBoxResult2 == DialogResult.Yes)
                        {
                            this.SaveProgress();
                        }
                        else if (msgBoxResult2 == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
            }
            this.lstNotes.BeginUpdate();
            this.lstNotes.Items.Clear();
            this.lstNotes.EndUpdate();
            this.sEndNoteArray = new ArrayList();
            this.sEndNoteInfo = new ArrayList();
            this.txtENText.Enabled = false;
            this.txtENText.Text = "";
            this.btnBreak.Enabled = false;
            this.btnPrev.Enabled = false;
            this.btnNext.Enabled = false;
            this.chkSupra.Enabled = false;
            this.rbBooks.Enabled = false;
            this.rbJournal.Enabled = false;
            this.rbCase.Enabled = false;
            this.rbPeriodical.Enabled = false;
            this.rbLegislative.Enabled = false;
            this.rbMiscellaneous.Enabled = false;
            this.gbxType.Enabled = false;
            this.mnOpen.Enabled = true;
            this.mnOpenPart.Enabled = true;
            this.mnClose.Enabled = false;
            this.mnExport.Enabled = false;
            this.mnSaveProg.Enabled = false;
        }

        private void ProcessingForm_Closing(object sender, CancelEventArgs e)
        {
            if (!this.bExitGenerated)
            {
                if (this.oldSelectedIndex >= 0 && this.sEndNoteArray != null && this.oldSelectedIndex < this.sEndNoteArray.Count)
                {
                    this.sEndNoteArray[oldSelectedIndex] = this.txtENText.Text;
                    NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo[oldSelectedIndex];
                    noteInfo.SupraOrId = this.chkSupra.Checked;
                    if (this.rbJournal.Checked)
                    {
                        noteInfo.Type = 0;
                    }
                    if (this.rbBooks.Checked)
                    {
                        noteInfo.Type = 1;
                    }
                    if (this.rbCase.Checked)
                    {
                        noteInfo.Type = 2;
                    }
                    if (this.rbLegislative.Checked)
                    {
                        noteInfo.Type = 3;
                    }
                    if (this.rbPeriodical.Checked)
                    {
                        noteInfo.Type = 4;
                    }
                    if (this.rbMiscellaneous.Checked)
                    {
                        noteInfo.Type = 5;
                    }
                    this.sEndNoteInfo[oldSelectedIndex] = noteInfo;
                }
                if (!this.bSaved & !this.bSavedProgress)
                {
                    DialogResult msgBoxResult = MessageBox.Show("You have not yet exported the endnotes, would you like to before closing?", "Processing Endnotes...", MessageBoxButtons.YesNo);
                    if (msgBoxResult == DialogResult.Yes)
                    {
                        this.ExportCSV();
                    }
                    else
                    {
                        DialogResult msgBoxResult2 = MessageBox.Show("Would you like to save your progress so that it can be resumed later?", "Processing Endnotes...", MessageBoxButtons.YesNo);
                        if (msgBoxResult2 == DialogResult.Yes)
                        {
                            this.SaveProgress();
                        }
                    }
                }
            }
        }

        private void mnSaveProg_Click(object sender, EventArgs e)
        {
            this.SaveProgress();
        }

        private void SaveProgress()
        {
            if (this.oldSelectedIndex >= 0)
            {
                this.sEndNoteArray[oldSelectedIndex] =  this.txtENText.Text;
                NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo[oldSelectedIndex];
                noteInfo.SupraOrId = this.chkSupra.Checked;
                if (this.rbJournal.Checked)
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.Checked)
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.Checked)
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.Checked)
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.Checked)
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.Checked)
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo[oldSelectedIndex] =  noteInfo;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Title = "Save the current progress...";
            saveFileDialog.Filter = "Partial Endnote Edit (*.pen)|*.pen";
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            checked
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
                        int num = this.sEndNoteArray.Count - 1;
                        for (int i = 0; i <= num; i++)
                        {
                            streamWriter.Write(sEndNoteArray[i]);
                            if (i < this.sEndNoteArray.Count - 1)
                            {
                                streamWriter.Write(this.sDelimiter2);
                            }
                        }
                        streamWriter.Write(this.sDelimiter3);
                        int num2 = this.sEndNoteInfo.Count - 1;
                        for (int i = 0; i <= num2; i++)
                        {
                            NoteInfo noteInfo = (NoteInfo)sEndNoteInfo[i];
                            streamWriter.Write(noteInfo.Type.ToString() + sDelimiter4 + noteInfo.SupraOrId.ToString());
                            if (i < this.sEndNoteInfo.Count - 1)
                            {
                                streamWriter.Write(this.sDelimiter2);
                            }
                        }
                        streamWriter.Close();
                        this.bSavedProgress = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There was an error saving the file, your information may not have been saved.", "Processing Endnotes...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void mnOpenPart_Click(object sender, EventArgs e)
        {
            if (this.oldSelectedIndex >= 0 && this.sEndNoteArray != null && this.oldSelectedIndex < this.sEndNoteArray.Count)
            {
                this.sEndNoteArray[oldSelectedIndex] = txtENText.Text;
                NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo[oldSelectedIndex];
                noteInfo.SupraOrId = this.chkSupra.Checked;
                if (this.rbJournal.Checked)
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.Checked)
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.Checked)
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.Checked)
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.Checked)
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.Checked)
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo[oldSelectedIndex] = noteInfo;
            }
            if (!this.bSaved & !this.bSavedProgress)
            {
                DialogResult msgBoxResult = MessageBox.Show("You have not yet exported the endnotes, would you like to before exiting?", "Processing Endnotes...", MessageBoxButtons.YesNoCancel);
                if (msgBoxResult == DialogResult.Yes)
                {
                    this.ExportCSV();
                }
                else
                {
                    if (msgBoxResult == DialogResult.Cancel)
                    {
                        return;
                    }
                    if (msgBoxResult == DialogResult.No)
                    {
                        DialogResult msgBoxResult2 = MessageBox.Show("Would you like to save your progress so that it can be resumed later?", "Processing Endnotes...", MessageBoxButtons.YesNoCancel);
                        if (msgBoxResult2 == DialogResult.Yes)
                        {
                            this.SaveProgress();
                        }
                        else if (msgBoxResult2 == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Open a work in progress...";
            openFileDialog.Filter = "Partial Endnote Edit (*.pen)|*.pen";
            openFileDialog.CheckFileExists = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.CheckPathExists = true;
            checked
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {/*
                    try
                    {
                        StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                        string text = streamReader.ReadLine();
                        string[] array = text.Split(this.sDelimiter3);
                        string[] array2 = array[0].Split(this.sDelimiter);
                        string[] array3 = array[1].Split(this.sDelimiter2);
                        this.sEndNoteArray = new ArrayList();
                        this.sEndNoteInfo = new ArrayList();
                        int num = array2.Length - 1;
                        NoteInfo noteInfo;
                        for (int i = 0; i <= num; i++)
                        {
                            this.sEndNoteArray.Add(array2[i]);
                            noteInfo = new NoteInfo();
                            string[] array4 = array3[i].Split(this.sDelimiter4);
                            noteInfo.Type = int.Parse(array4[0]);
                            noteInfo.SupraOrId = bool.Parse(array4[1]);
                            this.sEndNoteInfo.Add(noteInfo);
                        }
                        this.updateListBox();
                        this.txtENText.Text = (string)this.sEndNoteArray[lstNotes.SelectedIndex];
                        noteInfo = (NoteInfo)this.sEndNoteInfo[lstNotes.SelectedIndex];
                        this.chkSupra.Checked = noteInfo.SupraOrId;
                        this.oldSelectedIndex = this.lstNotes.SelectedIndex;
                        switch (noteInfo.Type)
                        {
                        case 0:
                            this.rbJournal.Checked = true;
                            break;
                        case 1:
                            this.rbBooks.Checked = true;
                            break;
                        case 2:
                            this.rbCase.Checked = true;
                            break;
                        case 3:
                            this.rbLegislative.Checked = true;
                            break;
                        case 4:
                            this.rbPeriodical.Checked = true;
                            break;
                        case 5:
                            this.rbMiscellaneous.Checked = true;
                            break;
                        }
                        this.txtENText.Enabled = true;
                        this.btnBreak.Enabled = true;
                        this.btnPrev.Enabled = true;
                        this.btnNext.Enabled = true;
                        this.chkSupra.Enabled = true;
                        this.rbBooks.Enabled = true;
                        this.rbJournal.Enabled = true;
                        this.rbCase.Enabled = true;
                        this.rbPeriodical.Enabled = true;
                        this.rbLegislative.Enabled = true;
                        this.rbMiscellaneous.Enabled = true;
                        this.gbxType.Enabled = true;
                        this.mnOpen.Enabled = false;
                        this.mnOpenPart.Enabled = false;
                        this.mnSaveProg.Enabled = true;
                        this.mnClose.Enabled = true;
                        this.mnExport.Enabled = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There was an error opening the file, it may be corrupt.", "Processing Endnotes...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                */}
            }
        }

        private void chkSupra_CheckStateChanged(object sender, EventArgs e)
        {
            if (!this.chkSupra.Checked)
            {
                this.rbBooks.Enabled = true;
                this.rbJournal.Enabled = true;
                this.rbCase.Enabled = true;
                this.rbPeriodical.Enabled = true;
                this.rbLegislative.Enabled = true;
                this.rbMiscellaneous.Enabled = true;
            }
            else
            {
                this.rbBooks.Enabled = false;
                this.rbJournal.Enabled = false;
                this.rbCase.Enabled = false;
                this.rbPeriodical.Enabled = false;
                this.rbLegislative.Enabled = false;
                this.rbMiscellaneous.Enabled = false;
            }
        }

        private void ProcessingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.E)
                {
                    this.chkSupra.Checked = !this.chkSupra.Checked;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.btnNext_Click(sender, e);
                    this.txtENText.Focus();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.btnPrev_Click(sender, e);
                    this.txtENText.Focus();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.U)
                {
                    this.btnBreak_Click(sender, e);
                    e.Handled = true;
                }
                else if (!this.chkSupra.Checked)
                {
                    if (e.KeyCode == Keys.B)
                    {
                        this.rbBooks.Checked = true;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.J)
                    {
                        this.rbJournal.Checked = true;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.C)
                    {
                        this.rbCase.Checked = true;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.M)
                    {
                        this.rbMiscellaneous.Checked = true;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.P)
                    {
                        this.rbPeriodical.Checked = true;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.L)
                    {
                        this.rbLegislative.Checked = true;
                        e.Handled = true;
                    }
                }
            }
            if (e.Modifiers == Keys.Alt)
            {
                if (e.KeyCode == Keys.O)
                {
                    this.mnOpen_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.E)
                {
                    this.mnExport_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.P)
                {
                    this.mnOpenPart_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.S)
                {
                    this.mnSaveProg_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.X)
                {
                    this.mnExit_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.C)
                {
                    this.mnClose_Click(sender, e);
                    e.Handled = true;
                }
            }
        }

        private void txtENText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Alt)
            {
                e.Handled = true;
            }
            else if (e.Modifiers == Keys.Control)
            {
                e.Handled = true;
            }
        }
    }
}
