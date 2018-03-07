using System.Collections.Generic;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace EndnoteExtractor
{
    public class EndnoteExtractor
    {
        public static List<string> GetEndnotes(string path)
        {
            List<string> vs = new List<string>();

            WordprocessingDocument oWordDoc = WordprocessingDocument.Open(path, false);
            Endnotes endnotes = oWordDoc.MainDocumentPart.EndnotesPart.Endnotes;
            foreach (Endnote endnote in endnotes)
            {
                vs.Add(endnote.InnerText.Trim());
            }
            oWordDoc.Close();

            return vs;
        }
    }
}
