using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;

namespace BusinessLayer.DataServices
{
    public static class MSWordParser
    {
        public static bool IsError { get; private set; }

        public static void SaveAsHtml(string docSrcFilePath, string htmlDestFilePath)
        {
            IsError = false;
            if (!File.Exists(docSrcFilePath)) return;

            Application docApp = null;
            string strExtension = Path.GetExtension(docSrcFilePath);
            try
            {
                docApp = new Application
                {
                    Visible = true,
                    DisplayAlerts = WdAlertLevel.wdAlertsNone
                };

                docApp.Application.Visible = true;
                var doc = docApp.Documents.Open(docSrcFilePath);
                doc.SaveAs2(htmlDestFilePath, WdSaveFormat.wdFormatHTML);
            }
            catch
            {
                IsError = true;
            }

            if (docApp == null) return;
            try
            {
                docApp.Quit(SaveChanges: false);
            }
            catch
            {
                // ignored
            }

            foreach (var process in Process.GetProcessesByName("WINWORD")) process.Kill();

            Marshal.ReleaseComObject(docApp);
            GC.Collect();
        }
    }
}