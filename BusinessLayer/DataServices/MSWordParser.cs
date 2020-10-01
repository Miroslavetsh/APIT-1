using System;
using Microsoft.Office.Interop.Word;

namespace BusinessLayer.DataServices
{
    public static class MSWordParser
    {
        public static void ConvertDocToHtml(object SourcePath, object TargetPath)
        {
            _Application newApp = new Application();
            var d = newApp.Documents;
            var Unknown = Type.Missing;
            var od = d.Open(ref SourcePath, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown, ref Unknown);
            object format = WdSaveFormat.wdFormatHTML;

            newApp.ActiveDocument.SaveAs(ref TargetPath, ref format,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown, ref Unknown,
                ref Unknown, ref Unknown);

            newApp.Documents.Close(WdSaveOptions.wdDoNotSaveChanges);
        }
    }
}