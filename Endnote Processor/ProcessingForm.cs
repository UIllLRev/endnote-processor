using Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FirstVistaTest
{
    public partial class ProcessingForm : Form
    {
        private Application oWordApp;

        private Document oWordDoc;

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
            Application.Run(new ProcessingForm());
        }

        public ProcessingForm()
        {
            base.add_Load(new EventHandler(this.ProcessingForm_Load));
            base.add_Closing(new CancelEventHandler(this.ProcessingForm_Closing));
            base.add_KeyDown(new KeyEventHandler(this.ProcessingForm_KeyDown));
            this.sDelimiter = "|*#*|";
            this.sDelimiter2 = "|*&*|";
            this.sDelimiter3 = "|*$*|";
            this.sDelimiter4 = "|*@*|";
            this.sDelimiter5 = "|*%*|";
            this.sDelimiter6 = "|*!*|";
            this.bSaved = true;
            this.bExitGenerated = false;
            this.bSavedProgress = true;
            this.isUpdate = false;
            this.oldSelectedIndex = 0;
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

        private void mnExit_Click(object sender, EventArgs e)
        {
            if (this.oldSelectedIndex >= 0 && this.sEndNoteArray != null && this.oldSelectedIndex < this.sEndNoteArray.get_Count())
            {
                this.sEndNoteArray.set_Item(this.oldSelectedIndex, this.txtENText.get_Text());
                NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.oldSelectedIndex);
                noteInfo.SupraOrId = this.chkSupra.get_Checked();
                if (this.rbJournal.get_Checked())
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.get_Checked())
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.get_Checked())
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.get_Checked())
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.get_Checked())
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.get_Checked())
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo.set_Item(this.oldSelectedIndex, noteInfo);
            }
            if (!this.bSaved & !this.bSavedProgress)
            {
                MsgBoxResult msgBoxResult = Interaction.MsgBox("You have not yet exported the endnotes, would you like to before exiting?", 3, "Processing Endnotes...");
                if (msgBoxResult == 6)
                {
                    this.ExportCSV();
                }
                else
                {
                    if (msgBoxResult == 2)
                    {
                        return;
                    }
                    if (msgBoxResult == 7)
                    {
                        MsgBoxResult msgBoxResult2 = Interaction.MsgBox("Would you like to save your progress so that it can be resumed later?", 3, "Processing Endnotes...");
                        if (msgBoxResult2 == 6)
                        {
                            this.SaveProgress();
                        }
                        else if (msgBoxResult2 == 2)
                        {
                            return;
                        }
                    }
                }
            }
            if (this.oWordApp != null)
            {
                _Application arg_180_0 = this.oWordApp;
                object value = Missing.Value;
                object value2 = Missing.Value;
                object value3 = Missing.Value;
                arg_180_0.Quit(ref value, ref value2, ref value3);
                this.oWordApp = null;
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
                    openFileDialog.set_Multiselect(false);
                    openFileDialog.set_InitialDirectory(Environment.GetFolderPath(5));
                    openFileDialog.set_Filter("Word Documents (*.doc; *.docx)|*.doc;*.docx");
                    openFileDialog.set_Title("Open a Word document to process...");
                    openFileDialog.set_CheckFileExists(true);
                    if (openFileDialog.ShowDialog() == 1)
                    {
                        Documents arg_E3_0 = this.oWordApp.get_Documents();
                        OpenFileDialog openFileDialog2 = openFileDialog;
                        object fileName = openFileDialog2.get_FileName();
                        object value = Missing.Value;
                        object value2 = Missing.Value;
                        object value3 = Missing.Value;
                        object value4 = Missing.Value;
                        object value5 = Missing.Value;
                        object value6 = Missing.Value;
                        object value7 = Missing.Value;
                        object value8 = Missing.Value;
                        object value9 = Missing.Value;
                        object value10 = Missing.Value;
                        object value11 = Missing.Value;
                        object value12 = Missing.Value;
                        object value13 = Missing.Value;
                        object value14 = Missing.Value;
                        object value15 = Missing.Value;
                        Document arg_F6_1 = arg_E3_0.Open(ref fileName, ref value, ref value2, ref value3, ref value4, ref value5, ref value6, ref value7, ref value8, ref value9, ref value10, ref value11, ref value12, ref value13, ref value14, ref value15);
                        openFileDialog2.set_FileName(StringType.FromObject(fileName));
                        this.oWordDoc = arg_F6_1;
                        if (this.oWordDoc.get_Endnotes().get_Count() > 0)
                        {
                            frmProgress frmProgress = new frmProgress();
                            frmProgress.Show();
                            frmProgress.SetMinVal(0);
                            frmProgress.SetMaxVal(this.oWordDoc.get_Endnotes().get_Count());
                            this.sEndNoteArray = new ArrayList();
                            this.sEndNoteInfo = new ArrayList();
                            int i = 0;
                            NoteInfo noteInfo = new NoteInfo();
                            int arg_16D_0 = 0;
                            int num = this.oWordDoc.get_Endnotes().get_Count() - 1;
                            for (i = arg_16D_0; i <= num; i++)
                            {
                                try
                                {
                                    if (this.oWordDoc.get_Endnotes().get_Item(i + 1).get_Range().get_Text() != null)
                                    {
                                        this.sEndNoteArray.Add(this.oWordDoc.get_Endnotes().get_Item(i + 1).get_Range().get_Text().Trim());
                                        this.sEndNoteInfo.Add(new NoteInfo());
                                        if (this.oWordDoc.get_Endnotes().get_Item(i + 1).get_Range().get_Text().Trim().ToLower().IndexOf("id.") >= 0 | this.oWordDoc.get_Endnotes().get_Item(i + 1).get_Range().get_Text().Trim().ToLower().IndexOf("supra") >= 0 | this.oWordDoc.get_Endnotes().get_Item(i + 1).get_Range().get_Text().Trim().ToLower().IndexOf("need cite") >= 0)
                                        {
                                            noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.sEndNoteInfo.get_Count() - 1);
                                            noteInfo.SupraOrId = true;
                                            this.sEndNoteInfo.set_Item(this.sEndNoteInfo.get_Count() - 1, noteInfo);
                                        }
                                        frmProgress.StepUp(1);
                                    }
                                }
                                catch (Exception expr_2C4)
                                {
                                    ProjectData.SetProjectError(expr_2C4);
                                    try
                                    {
                                        Interaction.MsgBox("There was an error in processing endnote #" + StringType.FromInteger(i + 1), 48, "Processing Endnotes");
                                        if (this.oWordDoc != null)
                                        {
                                            _Document arg_314_0 = this.oWordDoc;
                                            value15 = Missing.Value;
                                            value14 = Missing.Value;
                                            value13 = Missing.Value;
                                            arg_314_0.Close(ref value15, ref value14, ref value13);
                                            this.oWordDoc = null;
                                        }
                                    }
                                    catch (Exception expr_322)
                                    {
                                        ProjectData.SetProjectError(expr_322);
                                        ProjectData.ClearProjectError();
                                    }
                                    ProjectData.ClearProjectError();
                                    return;
                                }
                            }
                            _Document arg_368_0 = this.oWordDoc;
                            value15 = Missing.Value;
                            value14 = Missing.Value;
                            value13 = Missing.Value;
                            arg_368_0.Close(ref value15, ref value14, ref value13);
                            this.updateListBox();
                            if (frmProgress != null)
                            {
                                frmProgress.Close();
                                frmProgress = null;
                            }
                            this.txtENText.set_Text(StringType.FromObject(this.sEndNoteArray.get_Item(this.lstNotes.get_SelectedIndex())));
                            noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.lstNotes.get_SelectedIndex());
                            this.chkSupra.set_Checked(noteInfo.SupraOrId);
                            this.oldSelectedIndex = this.lstNotes.get_SelectedIndex();
                            switch (noteInfo.Type)
                            {
                            case 0:
                                this.rbJournal.set_Checked(true);
                                break;
                            case 1:
                                this.rbBooks.set_Checked(true);
                                break;
                            case 2:
                                this.rbCase.set_Checked(true);
                                break;
                            case 3:
                                this.rbLegislative.set_Checked(true);
                                break;
                            case 4:
                                this.rbPeriodical.set_Checked(true);
                                break;
                            case 5:
                                this.rbMiscellaneous.set_Checked(true);
                                break;
                            }
                            this.txtENText.set_Enabled(true);
                            this.btnBreak.set_Enabled(true);
                            this.btnPrev.set_Enabled(true);
                            this.btnNext.set_Enabled(true);
                            this.chkSupra.set_Enabled(true);
                            this.rbBooks.set_Enabled(true);
                            this.rbJournal.set_Enabled(true);
                            this.rbCase.set_Enabled(true);
                            this.rbPeriodical.set_Enabled(true);
                            this.rbLegislative.set_Enabled(true);
                            this.rbMiscellaneous.set_Enabled(true);
                            this.gbxType.set_Enabled(true);
                            this.mnOpen.set_Enabled(false);
                            this.mnOpenPart.set_Enabled(false);
                            this.mnClose.set_Enabled(true);
                            this.mnExport.set_Enabled(true);
                            this.mnSaveProg.set_Enabled(true);
                            this.bSaved = false;
                            this.bSavedProgress = false;
                        }
                        else
                        {
                            Interaction.MsgBox("There are no endnotes in this document.", 64, "Processing Endnotes");
                            _Document arg_568_0 = this.oWordDoc;
                            value15 = Missing.Value;
                            value14 = Missing.Value;
                            value13 = Missing.Value;
                            arg_568_0.Close(ref value15, ref value14, ref value13);
                            this.oWordDoc = null;
                        }
                    }
                }
                catch (Exception expr_578)
                {
                    ProjectData.SetProjectError(expr_578);
                    Interaction.MsgBox("There was an error opening the file, please check the file and try again.", 16, "Processing Endnotes");
                    try
                    {
                        if (this.oWordDoc != null)
                        {
                            _Document arg_5BB_0 = this.oWordDoc;
                            object value15 = Missing.Value;
                            object value14 = Missing.Value;
                            object value13 = Missing.Value;
                            arg_5BB_0.Close(ref value15, ref value14, ref value13);
                            this.oWordDoc = null;
                        }
                    }
                    catch (Exception expr_5C9)
                    {
                        ProjectData.SetProjectError(expr_5C9);
                        ProjectData.ClearProjectError();
                    }
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void ProcessingForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.oWordApp = new ApplicationClass();
                this.sEndNoteArray = new ArrayList();
                this.sEndNoteInfo = new ArrayList();
                this.mnClose.set_Enabled(false);
                this.mnOpen.set_Enabled(true);
                this.mnExport.set_Enabled(false);
                this.mnOpenPart.set_Enabled(true);
                this.mnSaveProg.set_Enabled(false);
            }
            catch (Exception expr_5F)
            {
                ProjectData.SetProjectError(expr_5F);
                Interaction.MsgBox("You must have Microsoft Word XP or higher installed to use this program", 16, "Processing Endnotes");
                this.Close();
                ProjectData.ClearProjectError();
            }
        }

        public void updateListBox()
        {
            this.isUpdate = true;
            this.lstNotes.BeginUpdate();
            int num = 0;
            try
            {
                num = this.lstNotes.get_SelectedIndex();
            }
            catch (Exception expr_22)
            {
                ProjectData.SetProjectError(expr_22);
                ProjectData.ClearProjectError();
            }
            if (num < 0)
            {
                num = 0;
            }
            this.lstNotes.get_Items().Clear();
            int arg_58_0 = 0;
            checked
            {
                int num2 = this.sEndNoteArray.get_Count() - 1;
                for (int i = arg_58_0; i <= num2; i++)
                {
                    NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(i);
                    this.lstNotes.get_Items().Add("Endnote " + (i + 1).ToString());
                }
                this.lstNotes.EndUpdate();
                this.lstNotes.set_SelectedIndex(num);
                this.isUpdate = false;
            }
        }

        private void lstNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.isUpdate)
            {
                NoteInfo noteInfo;
                if (this.oldSelectedIndex >= 0)
                {
                    this.sEndNoteArray.set_Item(this.oldSelectedIndex, this.txtENText.get_Text());
                    noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.oldSelectedIndex);
                    noteInfo.SupraOrId = this.chkSupra.get_Checked();
                    if (this.rbJournal.get_Checked())
                    {
                        noteInfo.Type = 0;
                    }
                    if (this.rbBooks.get_Checked())
                    {
                        noteInfo.Type = 1;
                    }
                    if (this.rbCase.get_Checked())
                    {
                        noteInfo.Type = 2;
                    }
                    if (this.rbLegislative.get_Checked())
                    {
                        noteInfo.Type = 3;
                    }
                    if (this.rbPeriodical.get_Checked())
                    {
                        noteInfo.Type = 4;
                    }
                    if (this.rbMiscellaneous.get_Checked())
                    {
                        noteInfo.Type = 5;
                    }
                    this.sEndNoteInfo.set_Item(this.oldSelectedIndex, noteInfo);
                }
                this.txtENText.set_Text(StringType.FromObject(this.sEndNoteArray.get_Item(this.lstNotes.get_SelectedIndex())));
                noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.lstNotes.get_SelectedIndex());
                this.chkSupra.set_Checked(noteInfo.SupraOrId);
                switch (noteInfo.Type)
                {
                case 0:
                    this.rbJournal.set_Checked(true);
                    break;
                case 1:
                    this.rbBooks.set_Checked(true);
                    break;
                case 2:
                    this.rbCase.set_Checked(true);
                    break;
                case 3:
                    this.rbLegislative.set_Checked(true);
                    break;
                case 4:
                    this.rbPeriodical.set_Checked(true);
                    break;
                case 5:
                    this.rbMiscellaneous.set_Checked(true);
                    break;
                }
                this.oldSelectedIndex = this.lstNotes.get_SelectedIndex();
                this.bSaved = false;
                this.bSavedProgress = false;
                this.txtENText.Focus();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.lstNotes.get_SelectedIndex() > 0)
            {
                ListBox lstNotes = this.lstNotes;
                lstNotes.set_SelectedIndex(checked(lstNotes.get_SelectedIndex() - 1));
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            checked
            {
                if (this.lstNotes.get_SelectedIndex() < this.lstNotes.get_Items().get_Count() - 1)
                {
                    ListBox lstNotes = this.lstNotes;
                    lstNotes.set_SelectedIndex(lstNotes.get_SelectedIndex() + 1);
                }
            }
        }

        private void btnBreak_Click(object sender, EventArgs e)
        {
            this.bSaved = false;
            this.bSavedProgress = false;
            NoteInfo noteInfo;
            if (this.oldSelectedIndex >= 0)
            {
                this.sEndNoteArray.set_Item(this.oldSelectedIndex, this.txtENText.get_Text());
                noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.oldSelectedIndex);
                noteInfo.SupraOrId = this.chkSupra.get_Checked();
                if (this.rbJournal.get_Checked())
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.get_Checked())
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.get_Checked())
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.get_Checked())
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.get_Checked())
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.get_Checked())
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo.set_Item(this.oldSelectedIndex, noteInfo);
            }
            new frmBreakUp
            {
                parentfrm = this,
                iIndex = this.lstNotes.get_SelectedIndex()
            }.ShowDialog();
            this.updateListBox();
            this.txtENText.set_Text(StringType.FromObject(this.sEndNoteArray.get_Item(this.lstNotes.get_SelectedIndex())));
            noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.lstNotes.get_SelectedIndex());
            this.chkSupra.set_Checked(noteInfo.SupraOrId);
            switch (noteInfo.Type)
            {
            case 0:
                this.rbJournal.set_Checked(true);
                break;
            case 1:
                this.rbBooks.set_Checked(true);
                break;
            case 2:
                this.rbCase.set_Checked(true);
                break;
            case 3:
                this.rbLegislative.set_Checked(true);
                break;
            case 4:
                this.rbPeriodical.set_Checked(true);
                break;
            case 5:
                this.rbMiscellaneous.set_Checked(true);
                break;
            }
            this.oldSelectedIndex = this.lstNotes.get_SelectedIndex();
        }

        private void mnExport_Click(object sender, EventArgs e)
        {
            if (this.oldSelectedIndex >= 0 && this.sEndNoteArray != null && this.oldSelectedIndex < this.sEndNoteArray.get_Count())
            {
                this.sEndNoteArray.set_Item(this.oldSelectedIndex, this.txtENText.get_Text());
                NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.oldSelectedIndex);
                noteInfo.SupraOrId = this.chkSupra.get_Checked();
                if (this.rbJournal.get_Checked())
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.get_Checked())
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.get_Checked())
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.get_Checked())
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.get_Checked())
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.get_Checked())
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo.set_Item(this.oldSelectedIndex, noteInfo);
            }
            this.ExportCSV();
        }

        private void ExportCSV()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.set_RestoreDirectory(true);
            saveFileDialog.set_Title("Save the export file collection...");
            saveFileDialog.set_Filter("Directory|");
            saveFileDialog.set_CheckFileExists(false);
            saveFileDialog.set_CheckPathExists(true);
            checked
            {
                if (saveFileDialog.ShowDialog() == 1)
                {
                    frmProgress frmProgress = new frmProgress();
                    frmProgress.SetMinVal(0);
                    frmProgress.SetMaxVal(this.sEndNoteArray.get_Count());
                    if (!Directory.Exists(saveFileDialog.get_FileName()))
                    {
                        Directory.CreateDirectory(saveFileDialog.get_FileName());
                    }
                    string text = saveFileDialog.get_FileName() + "\\";
                    ArrayList arrayList = new ArrayList();
                    ArrayList arrayList2 = new ArrayList();
                    ArrayList arrayList3 = new ArrayList();
                    ArrayList arrayList4 = new ArrayList();
                    ArrayList arrayList5 = new ArrayList();
                    ArrayList arrayList6 = new ArrayList();
                    int arg_C2_0 = 0;
                    int num = this.sEndNoteArray.get_Count() - 1;
                    for (int i = arg_C2_0; i <= num; i++)
                    {
                        NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(i);
                        if (!noteInfo.SupraOrId)
                        {
                            switch (noteInfo.Type)
                            {
                            case 0:
                                arrayList3.Add(RuntimeHelpers.GetObjectValue(this.sEndNoteArray.get_Item(i)));
                                break;
                            case 1:
                                arrayList.Add(RuntimeHelpers.GetObjectValue(this.sEndNoteArray.get_Item(i)));
                                break;
                            case 2:
                                arrayList2.Add(RuntimeHelpers.GetObjectValue(this.sEndNoteArray.get_Item(i)));
                                break;
                            case 3:
                                arrayList4.Add(RuntimeHelpers.GetObjectValue(this.sEndNoteArray.get_Item(i)));
                                break;
                            case 4:
                                arrayList5.Add(RuntimeHelpers.GetObjectValue(this.sEndNoteArray.get_Item(i)));
                                break;
                            case 5:
                                arrayList6.Add(RuntimeHelpers.GetObjectValue(this.sEndNoteArray.get_Item(i)));
                                break;
                            }
                        }
                        frmProgress.StepUp(1);
                    }
                    if (arrayList3.get_Count() > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList3.get_Count());
                        StreamWriter streamWriter = new StreamWriter(text + "journals.csv", false);
                        int arg_200_0 = 0;
                        int num2 = arrayList3.get_Count() - 1;
                        for (int i = arg_200_0; i <= num2; i++)
                        {
                            streamWriter.Write(ObjectType.StrCatObj(arrayList3.get_Item(i), this.sDelimiter));
                            frmProgress.StepUp(1);
                        }
                        streamWriter.Close();
                    }
                    if (arrayList.get_Count() > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList.get_Count());
                        StreamWriter streamWriter2 = new StreamWriter(text + "books.csv", false);
                        int arg_271_0 = 0;
                        int num3 = arrayList.get_Count() - 1;
                        for (int i = arg_271_0; i <= num3; i++)
                        {
                            streamWriter2.Write(ObjectType.StrCatObj(arrayList.get_Item(i), this.sDelimiter));
                            frmProgress.StepUp(1);
                        }
                        streamWriter2.Close();
                    }
                    if (arrayList2.get_Count() > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList2.get_Count());
                        StreamWriter streamWriter3 = new StreamWriter(text + "cases.csv", false);
                        int arg_2E2_0 = 0;
                        int num4 = arrayList2.get_Count() - 1;
                        for (int i = arg_2E2_0; i <= num4; i++)
                        {
                            streamWriter3.Write(ObjectType.StrCatObj(arrayList2.get_Item(i), this.sDelimiter));
                            frmProgress.StepUp(1);
                        }
                        streamWriter3.Close();
                    }
                    if (arrayList4.get_Count() > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList4.get_Count());
                        StreamWriter streamWriter4 = new StreamWriter(text + "legislative.csv", false);
                        int arg_353_0 = 0;
                        int num5 = arrayList4.get_Count() - 1;
                        for (int i = arg_353_0; i <= num5; i++)
                        {
                            streamWriter4.Write(ObjectType.StrCatObj(arrayList4.get_Item(i), this.sDelimiter));
                            frmProgress.StepUp(1);
                        }
                        streamWriter4.Close();
                    }
                    if (arrayList5.get_Count() > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList5.get_Count());
                        StreamWriter streamWriter5 = new StreamWriter(text + "periodicals.csv", false);
                        int arg_3C4_0 = 0;
                        int num6 = arrayList5.get_Count() - 1;
                        for (int i = arg_3C4_0; i <= num6; i++)
                        {
                            streamWriter5.Write(ObjectType.StrCatObj(arrayList5.get_Item(i), this.sDelimiter));
                            frmProgress.StepUp(1);
                        }
                        streamWriter5.Close();
                    }
                    if (arrayList6.get_Count() > 0)
                    {
                        frmProgress.ResetBar();
                        frmProgress.SetMaxVal(arrayList6.get_Count());
                        StreamWriter streamWriter6 = new StreamWriter(text + "miscellaneous.csv", false);
                        int arg_435_0 = 0;
                        int num7 = arrayList6.get_Count() - 1;
                        for (int i = arg_435_0; i <= num7; i++)
                        {
                            streamWriter6.Write(ObjectType.StrCatObj(arrayList6.get_Item(i), this.sDelimiter));
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
                this.sEndNoteArray.set_Item(this.oldSelectedIndex, this.txtENText.get_Text());
                NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.oldSelectedIndex);
                noteInfo.SupraOrId = this.chkSupra.get_Checked();
                if (this.rbJournal.get_Checked())
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.get_Checked())
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.get_Checked())
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.get_Checked())
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.get_Checked())
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.get_Checked())
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo.set_Item(this.oldSelectedIndex, noteInfo);
            }
            if (!this.bSaved & !this.bSavedProgress)
            {
                MsgBoxResult msgBoxResult = Interaction.MsgBox("You have not yet exported the endnotes, would you like to before closing?", 3, "Processing Endnotes...");
                if (msgBoxResult == 6)
                {
                    this.ExportCSV();
                }
                else
                {
                    if (msgBoxResult == 2)
                    {
                        return;
                    }
                    if (msgBoxResult == 7)
                    {
                        MsgBoxResult msgBoxResult2 = Interaction.MsgBox("Would you like to save your progress so that it can be resumed later?", 3, "Processing Endnotes...");
                        if (msgBoxResult2 == 6)
                        {
                            this.SaveProgress();
                        }
                        else if (msgBoxResult2 == 2)
                        {
                            return;
                        }
                    }
                }
            }
            this.lstNotes.BeginUpdate();
            this.lstNotes.get_Items().Clear();
            this.lstNotes.EndUpdate();
            this.sEndNoteArray = new ArrayList();
            this.sEndNoteInfo = new ArrayList();
            this.txtENText.set_Enabled(false);
            this.txtENText.set_Text("");
            this.btnBreak.set_Enabled(false);
            this.btnPrev.set_Enabled(false);
            this.btnNext.set_Enabled(false);
            this.chkSupra.set_Enabled(false);
            this.rbBooks.set_Enabled(false);
            this.rbJournal.set_Enabled(false);
            this.rbCase.set_Enabled(false);
            this.rbPeriodical.set_Enabled(false);
            this.rbLegislative.set_Enabled(false);
            this.rbMiscellaneous.set_Enabled(false);
            this.gbxType.set_Enabled(false);
            this.mnOpen.set_Enabled(true);
            this.mnOpenPart.set_Enabled(true);
            this.mnClose.set_Enabled(false);
            this.mnExport.set_Enabled(false);
            this.mnSaveProg.set_Enabled(false);
        }

        private void ProcessingForm_Closing(object sender, CancelEventArgs e)
        {
            if (!this.bExitGenerated)
            {
                if (this.oldSelectedIndex >= 0 && this.sEndNoteArray != null && this.oldSelectedIndex < this.sEndNoteArray.get_Count())
                {
                    this.sEndNoteArray.set_Item(this.oldSelectedIndex, this.txtENText.get_Text());
                    NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.oldSelectedIndex);
                    noteInfo.SupraOrId = this.chkSupra.get_Checked();
                    if (this.rbJournal.get_Checked())
                    {
                        noteInfo.Type = 0;
                    }
                    if (this.rbBooks.get_Checked())
                    {
                        noteInfo.Type = 1;
                    }
                    if (this.rbCase.get_Checked())
                    {
                        noteInfo.Type = 2;
                    }
                    if (this.rbLegislative.get_Checked())
                    {
                        noteInfo.Type = 3;
                    }
                    if (this.rbPeriodical.get_Checked())
                    {
                        noteInfo.Type = 4;
                    }
                    if (this.rbMiscellaneous.get_Checked())
                    {
                        noteInfo.Type = 5;
                    }
                    this.sEndNoteInfo.set_Item(this.oldSelectedIndex, noteInfo);
                }
                if (!this.bSaved & !this.bSavedProgress)
                {
                    MsgBoxResult msgBoxResult = Interaction.MsgBox("You have not yet exported the endnotes, would you like to before closing?", 4, "Processing Endnotes...");
                    if (msgBoxResult == 6)
                    {
                        this.ExportCSV();
                    }
                    else
                    {
                        MsgBoxResult msgBoxResult2 = Interaction.MsgBox("Would you like to save your progress so that it can be resumed later?", 4, "Processing Endnotes...");
                        if (msgBoxResult2 == 6)
                        {
                            this.SaveProgress();
                        }
                    }
                }
                if (this.oWordApp != null)
                {
                    _Application arg_17B_0 = this.oWordApp;
                    object value = Missing.Value;
                    object value2 = Missing.Value;
                    object value3 = Missing.Value;
                    arg_17B_0.Quit(ref value, ref value2, ref value3);
                    this.oWordApp = null;
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
                this.sEndNoteArray.set_Item(this.oldSelectedIndex, this.txtENText.get_Text());
                NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.oldSelectedIndex);
                noteInfo.SupraOrId = this.chkSupra.get_Checked();
                if (this.rbJournal.get_Checked())
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.get_Checked())
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.get_Checked())
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.get_Checked())
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.get_Checked())
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.get_Checked())
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo.set_Item(this.oldSelectedIndex, noteInfo);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.set_RestoreDirectory(true);
            saveFileDialog.set_InitialDirectory(Environment.GetFolderPath(5));
            saveFileDialog.set_Title("Save the current progress...");
            saveFileDialog.set_Filter("Partial Endnote Edit (*.pen)|*.pen");
            saveFileDialog.set_CheckFileExists(false);
            saveFileDialog.set_CheckPathExists(true);
            checked
            {
                if (saveFileDialog.ShowDialog() == 1)
                {
                    try
                    {
                        StreamWriter streamWriter = new StreamWriter(saveFileDialog.get_FileName(), false);
                        int arg_140_0 = 0;
                        int num = this.sEndNoteArray.get_Count() - 1;
                        for (int i = arg_140_0; i <= num; i++)
                        {
                            streamWriter.Write(RuntimeHelpers.GetObjectValue(this.sEndNoteArray.get_Item(i)));
                            if (i < this.sEndNoteArray.get_Count() - 1)
                            {
                                streamWriter.Write(this.sDelimiter2);
                            }
                        }
                        streamWriter.Write(this.sDelimiter3);
                        int arg_19B_0 = 0;
                        int num2 = this.sEndNoteInfo.get_Count() - 1;
                        for (int i = arg_19B_0; i <= num2; i++)
                        {
                            NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(i);
                            streamWriter.Write(StringType.FromInteger(noteInfo.Type) + this.sDelimiter4 + StringType.FromBoolean(noteInfo.SupraOrId));
                            if (i < this.sEndNoteInfo.get_Count() - 1)
                            {
                                streamWriter.Write(this.sDelimiter2);
                            }
                        }
                        streamWriter.Close();
                        this.bSavedProgress = true;
                    }
                    catch (Exception expr_20B)
                    {
                        ProjectData.SetProjectError(expr_20B);
                        Interaction.MsgBox("There was an error saving the file, your information may not have been saved.", 16, "Processing Endnotes...");
                        ProjectData.ClearProjectError();
                    }
                }
            }
        }

        private void mnOpenPart_Click(object sender, EventArgs e)
        {
            if (this.oldSelectedIndex >= 0 && this.sEndNoteArray != null && this.oldSelectedIndex < this.sEndNoteArray.get_Count())
            {
                this.sEndNoteArray.set_Item(this.oldSelectedIndex, this.txtENText.get_Text());
                NoteInfo noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.oldSelectedIndex);
                noteInfo.SupraOrId = this.chkSupra.get_Checked();
                if (this.rbJournal.get_Checked())
                {
                    noteInfo.Type = 0;
                }
                if (this.rbBooks.get_Checked())
                {
                    noteInfo.Type = 1;
                }
                if (this.rbCase.get_Checked())
                {
                    noteInfo.Type = 2;
                }
                if (this.rbLegislative.get_Checked())
                {
                    noteInfo.Type = 3;
                }
                if (this.rbPeriodical.get_Checked())
                {
                    noteInfo.Type = 4;
                }
                if (this.rbMiscellaneous.get_Checked())
                {
                    noteInfo.Type = 5;
                }
                this.sEndNoteInfo.set_Item(this.oldSelectedIndex, noteInfo);
            }
            if (!this.bSaved & !this.bSavedProgress)
            {
                MsgBoxResult msgBoxResult = Interaction.MsgBox("You have not yet exported the endnotes, would you like to before exiting?", 3, "Processing Endnotes...");
                if (msgBoxResult == 6)
                {
                    this.ExportCSV();
                }
                else
                {
                    if (msgBoxResult == 2)
                    {
                        return;
                    }
                    if (msgBoxResult == 7)
                    {
                        MsgBoxResult msgBoxResult2 = Interaction.MsgBox("Would you like to save your progress so that it can be resumed later?", 3, "Processing Endnotes...");
                        if (msgBoxResult2 == 6)
                        {
                            this.SaveProgress();
                        }
                        else if (msgBoxResult2 == 2)
                        {
                            return;
                        }
                    }
                }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.set_RestoreDirectory(true);
            openFileDialog.set_Title("Open a work in progress...");
            openFileDialog.set_Filter("Partial Endnote Edit (*.pen)|*.pen");
            openFileDialog.set_CheckFileExists(true);
            openFileDialog.set_InitialDirectory(Environment.GetFolderPath(5));
            openFileDialog.set_CheckPathExists(true);
            checked
            {
                if (openFileDialog.ShowDialog() == 1)
                {
                    try
                    {
                        StreamReader streamReader = new StreamReader(openFileDialog.get_FileName());
                        string text = streamReader.ReadLine();
                        string[] array = Strings.Split(text, this.sDelimiter3, -1, 0);
                        string[] array2 = Strings.Split(array[0], this.sDelimiter2, -1, 0);
                        string[] array3 = Strings.Split(array[1], this.sDelimiter2, -1, 0);
                        this.sEndNoteArray = new ArrayList();
                        this.sEndNoteInfo = new ArrayList();
                        int arg_210_0 = 0;
                        int num = array2.get_Length() - 1;
                        NoteInfo noteInfo;
                        for (int i = arg_210_0; i <= num; i++)
                        {
                            this.sEndNoteArray.Add(array2[i]);
                            noteInfo = new NoteInfo();
                            string[] array4 = Strings.Split(array3[i], this.sDelimiter4, -1, 0);
                            noteInfo.Type = int.Parse(array4[0]);
                            noteInfo.SupraOrId = bool.Parse(array4[1]);
                            this.sEndNoteInfo.Add(noteInfo);
                        }
                        this.updateListBox();
                        this.txtENText.set_Text(StringType.FromObject(this.sEndNoteArray.get_Item(this.lstNotes.get_SelectedIndex())));
                        noteInfo = (NoteInfo)this.sEndNoteInfo.get_Item(this.lstNotes.get_SelectedIndex());
                        this.chkSupra.set_Checked(noteInfo.SupraOrId);
                        this.oldSelectedIndex = this.lstNotes.get_SelectedIndex();
                        switch (noteInfo.Type)
                        {
                        case 0:
                            this.rbJournal.set_Checked(true);
                            break;
                        case 1:
                            this.rbBooks.set_Checked(true);
                            break;
                        case 2:
                            this.rbCase.set_Checked(true);
                            break;
                        case 3:
                            this.rbLegislative.set_Checked(true);
                            break;
                        case 4:
                            this.rbPeriodical.set_Checked(true);
                            break;
                        case 5:
                            this.rbMiscellaneous.set_Checked(true);
                            break;
                        }
                        this.txtENText.set_Enabled(true);
                        this.btnBreak.set_Enabled(true);
                        this.btnPrev.set_Enabled(true);
                        this.btnNext.set_Enabled(true);
                        this.chkSupra.set_Enabled(true);
                        this.rbBooks.set_Enabled(true);
                        this.rbJournal.set_Enabled(true);
                        this.rbCase.set_Enabled(true);
                        this.rbPeriodical.set_Enabled(true);
                        this.rbLegislative.set_Enabled(true);
                        this.rbMiscellaneous.set_Enabled(true);
                        this.gbxType.set_Enabled(true);
                        this.mnOpen.set_Enabled(false);
                        this.mnOpenPart.set_Enabled(false);
                        this.mnSaveProg.set_Enabled(true);
                        this.mnClose.set_Enabled(true);
                        this.mnExport.set_Enabled(true);
                    }
                    catch (Exception expr_425)
                    {
                        ProjectData.SetProjectError(expr_425);
                        Interaction.MsgBox("There was an error opening the file, it may be corrupt.", 16, "Processing Endnotes...");
                        ProjectData.ClearProjectError();
                    }
                }
            }
        }

        private void chkSupra_CheckStateChanged(object sender, EventArgs e)
        {
            if (!this.chkSupra.get_Checked())
            {
                this.rbBooks.set_Enabled(true);
                this.rbJournal.set_Enabled(true);
                this.rbCase.set_Enabled(true);
                this.rbPeriodical.set_Enabled(true);
                this.rbLegislative.set_Enabled(true);
                this.rbMiscellaneous.set_Enabled(true);
            }
            else
            {
                this.rbBooks.set_Enabled(false);
                this.rbJournal.set_Enabled(false);
                this.rbCase.set_Enabled(false);
                this.rbPeriodical.set_Enabled(false);
                this.rbLegislative.set_Enabled(false);
                this.rbMiscellaneous.set_Enabled(false);
            }
        }

        private void ProcessingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.get_Modifiers() == 131072)
            {
                if (e.get_KeyCode() == 69)
                {
                    this.chkSupra.set_Checked(!this.chkSupra.get_Checked());
                    e.set_Handled(true);
                }
                else if (e.get_KeyCode() == 40)
                {
                    this.btnNext_Click(RuntimeHelpers.GetObjectValue(sender), e);
                    this.txtENText.Focus();
                    e.set_Handled(true);
                }
                else if (e.get_KeyCode() == 38)
                {
                    this.btnPrev_Click(RuntimeHelpers.GetObjectValue(sender), e);
                    this.txtENText.Focus();
                    e.set_Handled(true);
                }
                else if (e.get_KeyCode() == 85)
                {
                    this.btnBreak_Click(RuntimeHelpers.GetObjectValue(sender), e);
                    e.set_Handled(true);
                }
                else if (!this.chkSupra.get_Checked())
                {
                    if (e.get_KeyCode() == 66)
                    {
                        this.rbBooks.set_Checked(true);
                        e.set_Handled(true);
                    }
                    else if (e.get_KeyCode() == 74)
                    {
                        this.rbJournal.set_Checked(true);
                        e.set_Handled(true);
                    }
                    else if (e.get_KeyCode() == 67)
                    {
                        this.rbCase.set_Checked(true);
                        e.set_Handled(true);
                    }
                    else if (e.get_KeyCode() == 77)
                    {
                        this.rbMiscellaneous.set_Checked(true);
                        e.set_Handled(true);
                    }
                    else if (e.get_KeyCode() == 80)
                    {
                        this.rbPeriodical.set_Checked(true);
                        e.set_Handled(true);
                    }
                    else if (e.get_KeyCode() == 76)
                    {
                        this.rbLegislative.set_Checked(true);
                        e.set_Handled(true);
                    }
                }
            }
            if (e.get_Modifiers() == 262144)
            {
                if (e.get_KeyCode() == 79)
                {
                    this.mnOpen_Click(RuntimeHelpers.GetObjectValue(sender), e);
                    e.set_Handled(true);
                }
                else if (e.get_KeyCode() == 69)
                {
                    this.mnExport_Click(RuntimeHelpers.GetObjectValue(sender), e);
                    e.set_Handled(true);
                }
                else if (e.get_KeyCode() == 80)
                {
                    this.mnOpenPart_Click(RuntimeHelpers.GetObjectValue(sender), e);
                    e.set_Handled(true);
                }
                else if (e.get_KeyCode() == 83)
                {
                    this.mnSaveProg_Click(RuntimeHelpers.GetObjectValue(sender), e);
                    e.set_Handled(true);
                }
                else if (e.get_KeyCode() == 115 || e.get_KeyCode() == 88)
                {
                    this.mnExit_Click(RuntimeHelpers.GetObjectValue(sender), e);
                    e.set_Handled(true);
                }
                else if (e.get_KeyCode() == 67)
                {
                    this.mnClose_Click(RuntimeHelpers.GetObjectValue(sender), e);
                    e.set_Handled(true);
                }
            }
        }

        private void txtENText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.get_Modifiers() == 262144)
            {
                e.set_Handled(true);
            }
            else if (e.get_Modifiers() == 131072)
            {
                e.set_Handled(true);
            }
        }
    }
}
