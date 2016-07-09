using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

using DocumentFormat.OpenXml.Packaging;


namespace EndnoteProcessor
{
    public partial class ProcessingForm : Form
    {
        private WordprocessingDocument oWordDoc;

        public List<string> sEndNoteArray;

        public List<NoteInfo> sEndNoteInfo;

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
            bSaved = true;
            bExitGenerated = false;
            bSavedProgress = true;
            isUpdate = false;
            oldSelectedIndex = 0;
            InitializeComponent();
        }

        private void SaveEndnote()
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

        private void mnExit_Click(object sender, EventArgs e)
        {
            if (oldSelectedIndex >= 0 && sEndNoteArray != null && oldSelectedIndex < sEndNoteArray.Count)
            {
                SaveEndnote();
            }
            if (!bSaved & !bSavedProgress)
            {
                DialogResult msgBoxResult = MessageBox.Show("You have not yet exported the endnotes, would you like to before exiting?", "Processing Endnotes...", MessageBoxButtons.YesNoCancel);
                if (msgBoxResult == DialogResult.Yes)
                {
                    Export();
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
                            SaveProgress();
                        }
                        else if (msgBoxResult2 == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
            }

            bExitGenerated = true;
            Close();
        }

        private void mnOpen_Click(object sender, EventArgs e)
        {
            checked
            {
                try
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = "Word Documents|*.docx";
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
                            sEndNoteArray = new List<string>();
                            sEndNoteInfo = new List<NoteInfo>();
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
                                            noteInfo = (NoteInfo)sEndNoteInfo[sEndNoteInfo.Count - 1];
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
                            oWordDoc = null;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There was an error opening the file, please check the file and try again.", "Processing Endnotes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    try
                    {
                        if (oWordDoc != null)
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
                sEndNoteArray = new List<string>();
                sEndNoteInfo = new List<NoteInfo>();
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
            lstNotes.Items.Clear();
            checked
            {
                int num2 = sEndNoteArray.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    NoteInfo noteInfo = (NoteInfo)sEndNoteInfo[i];
                    lstNotes.Items.Add("Endnote " + (i + 1).ToString());
                }
                lstNotes.EndUpdate();
                lstNotes.SelectedIndex = num;
                isUpdate = false;
            }
        }

        private void lstNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isUpdate)
            {
                NoteInfo noteInfo;
                if (oldSelectedIndex >= 0)
                {
                    SaveEndnote();
                }
                txtENText.Text = (string)sEndNoteArray[lstNotes.SelectedIndex];
                noteInfo = (NoteInfo)sEndNoteInfo[lstNotes.SelectedIndex];
                chkSupra.Checked = noteInfo.SupraOrId;
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
                oldSelectedIndex = lstNotes.SelectedIndex;
                bSaved = false;
                bSavedProgress = false;
                txtENText.Focus();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (lstNotes.SelectedIndex > 0)
            {
                ListBox lstNotes = this.lstNotes;
                lstNotes.SelectedIndex = lstNotes.SelectedIndex - 1;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            checked
            {
                if (lstNotes.SelectedIndex < lstNotes.Items.Count - 1)
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
                SaveEndnote();
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
            oldSelectedIndex = lstNotes.SelectedIndex;
        }

        private void mnExport_Click(object sender, EventArgs e)
        {
            if (oldSelectedIndex >= 0 && sEndNoteArray != null && oldSelectedIndex < sEndNoteArray.Count)
            {
                SaveEndnote();
            }

            Export();
        }

        private void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Save the export file collection...";
            saveFileDialog.Filter = "JSON|*.json";
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            checked
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    frmProgress frmProgress = new frmProgress();
                    frmProgress.SetMinVal(0);
                    frmProgress.SetMaxVal(sEndNoteArray.Count);

                    List<NoteExportInfo> exports = new List<NoteExportInfo>();
                    for (int i = 0; i < sEndNoteArray.Count; i++)
                    {
                        NoteInfo noteInfo = (NoteInfo)sEndNoteInfo[i];
                        if (!noteInfo.SupraOrId)
                        {
                            NoteExportInfo.Type t;
                            switch (noteInfo.Type)
                            {
                            case 0:
                                    t = NoteExportInfo.Type.J;
                                break;
                            case 1:
                                    t = NoteExportInfo.Type.B;
                                break;
                            case 2:
                                    t = NoteExportInfo.Type.C;
                                break;
                            case 3:
                                    t = NoteExportInfo.Type.L;
                                break;
                            case 4:
                                    t = NoteExportInfo.Type.P;
                                break;
                            default:
                                    t = NoteExportInfo.Type.M;
                                break;
                            }

                            exports.Add(new NoteExportInfo() { SourceType = t, Citation = sEndNoteArray[i] });
                        }
                        frmProgress.StepUp(1);
                    }

                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    { 
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<NoteExportInfo>));
                        serializer.WriteObject(fs, exports);
                    }

                    bSaved = true;
                    bSavedProgress = true;
                    if (frmProgress != null)
                    {
                        frmProgress.Close();
                    }
                }
            }
        }

        private void mnClose_Click(object sender, EventArgs e)
        {
            if (oldSelectedIndex >= 0)
            {
                SaveEndnote();
            }

            if (!bSaved & !bSavedProgress)
            {
                DialogResult msgBoxResult = MessageBox.Show("You have not yet exported the endnotes, would you like to before closing?", "Processing Endnotes...", MessageBoxButtons.YesNoCancel);
                if (msgBoxResult == DialogResult.Yes)
                {
                    Export();
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
                            SaveProgress();
                        }
                        else if (msgBoxResult2 == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
            }
            lstNotes.BeginUpdate();
            lstNotes.Items.Clear();
            lstNotes.EndUpdate();
            sEndNoteArray = new List<string>();
            sEndNoteInfo = new List<NoteInfo>();
            txtENText.Enabled = false;
            txtENText.Text = "";
            btnBreak.Enabled = false;
            btnPrev.Enabled = false;
            btnNext.Enabled = false;
            chkSupra.Enabled = false;
            rbBooks.Enabled = false;
            rbJournal.Enabled = false;
            rbCase.Enabled = false;
            rbPeriodical.Enabled = false;
            rbLegislative.Enabled = false;
            rbMiscellaneous.Enabled = false;
            gbxType.Enabled = false;
            mnOpen.Enabled = true;
            mnOpenPart.Enabled = true;
            mnClose.Enabled = false;
            mnExport.Enabled = false;
            mnSaveProg.Enabled = false;
        }

        private void ProcessingForm_Closing(object sender, CancelEventArgs e)
        {
            if (!bExitGenerated)
            {
                if (oldSelectedIndex >= 0 && sEndNoteArray != null && oldSelectedIndex < sEndNoteArray.Count)
                {
                    SaveEndnote();
                }

                if (!bSaved & !bSavedProgress)
                {
                    DialogResult msgBoxResult = MessageBox.Show("You have not yet exported the endnotes, would you like to before closing?", "Processing Endnotes...", MessageBoxButtons.YesNo);
                    if (msgBoxResult == DialogResult.Yes)
                    {
                        Export();
                    }
                    else
                    {
                        DialogResult msgBoxResult2 = MessageBox.Show("Would you like to save your progress so that it can be resumed later?", "Processing Endnotes...", MessageBoxButtons.YesNo);
                        if (msgBoxResult2 == DialogResult.Yes)
                        {
                            SaveProgress();
                        }
                    }
                }
            }
        }

        private void mnSaveProg_Click(object sender, EventArgs e)
        {
            SaveProgress();
        }

        private void SaveProgress()
        {
            if (oldSelectedIndex >= 0)
            {
                SaveEndnote();
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
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
                        using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                        {
                            XmlWriter xmlWriter = XmlWriter.Create(fs);
                            xmlWriter.WriteStartElement("EndnoteProcessorState");
                            DataContractSerializer endnoteSerializer = new DataContractSerializer(typeof(List<string>));
                            endnoteSerializer.WriteObject(xmlWriter, sEndNoteArray);
                            DataContractSerializer infoSerializer = new DataContractSerializer(typeof(List<NoteInfo>));
                            infoSerializer.WriteObject(xmlWriter, sEndNoteInfo);
                            xmlWriter.WriteEndElement();
                            xmlWriter.Close();
                        }
                        bSavedProgress = true;
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
            if (oldSelectedIndex >= 0 && sEndNoteArray != null && oldSelectedIndex < sEndNoteArray.Count)
            {
                SaveEndnote();
            }

            if (!bSaved & !bSavedProgress)
            {
                DialogResult msgBoxResult = MessageBox.Show("You have not yet exported the endnotes, would you like to before exiting?", "Processing Endnotes...", MessageBoxButtons.YesNoCancel);
                if (msgBoxResult == DialogResult.Yes)
                {
                    Export();
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
                            SaveProgress();
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
            openFileDialog.Filter = "Partial Endnote Edit|*.pen";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            checked
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                        {
                            XmlDictionaryReader xmlReader = XmlDictionaryReader.CreateTextReader(fs, XmlDictionaryReaderQuotas.Max);
                            xmlReader.ReadStartElement();
                            DataContractSerializer endnoteSerializer = new DataContractSerializer(typeof(List<string>));
                            sEndNoteArray = (List<string>)endnoteSerializer.ReadObject(xmlReader);
                            DataContractSerializer infoSerializer = new DataContractSerializer(typeof(List<NoteInfo>));
                            sEndNoteInfo = (List<NoteInfo>)infoSerializer.ReadObject(xmlReader);
                        }

                        updateListBox();
                        txtENText.Text = (string)sEndNoteArray[lstNotes.SelectedIndex];
                        NoteInfo noteInfo = (NoteInfo)sEndNoteInfo[lstNotes.SelectedIndex];
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
                        mnSaveProg.Enabled = true;
                        mnClose.Enabled = true;
                        mnExport.Enabled = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There was an error opening the file, it may be corrupt.", "Processing Endnotes...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void chkSupra_CheckStateChanged(object sender, EventArgs e)
        {
            if (!chkSupra.Checked)
            {
                rbBooks.Enabled = true;
                rbJournal.Enabled = true;
                rbCase.Enabled = true;
                rbPeriodical.Enabled = true;
                rbLegislative.Enabled = true;
                rbMiscellaneous.Enabled = true;
            }
            else
            {
                rbBooks.Enabled = false;
                rbJournal.Enabled = false;
                rbCase.Enabled = false;
                rbPeriodical.Enabled = false;
                rbLegislative.Enabled = false;
                rbMiscellaneous.Enabled = false;
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
