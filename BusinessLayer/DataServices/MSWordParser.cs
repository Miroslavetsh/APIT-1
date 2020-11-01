using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aspose.Words;
using SautinSoft;

namespace BusinessLayer.DataServices
{
    /// <summary>
    /// Static class, which is intended exclusively
    /// for working with MS Word document files
    /// </summary>
    public static class MSWordParser
    {
        /// <summary>
        /// Converts a document from a source to a different
        /// type and saves it to the destination path
        /// 
        /// *At the moment, full support only for the html format,
        /// but in the future it is possible to expand the functionality
        /// </summary>
        /// <param name="docSrcFile">Source path with 100% existing .doc/.docx file</param>
        /// <param name="destFile">Absolute destination path</param>
        /// <param name="format">Destination data format</param>
        public static async Task Save(string docSrcFile, string destFile, RtfToHtml.eOutputFormat format)
        {
            var util = new RtfToHtml();
            util.OpenDocx(docSrcFile);
            util.OutputFormat = format;

            // read HTML from file to change it
            var stream = File.Create(destFile);
            stream.Close();

            // convert DOCX as a HTML
            util.ToHtml(destFile);

            // read HTML from file to change it
            using var reader = new StreamReader(destFile);
            string htmlText = await reader.ReadToEndAsync();
            reader.Close();

            CleanHtmlContent(ref htmlText);
            RegexFixHtml(ref htmlText);
            RemoveTags(ref htmlText, "script");

            // save result HTML back to the file
            await using var writer = new StreamWriter(destFile);
            await writer.WriteAsync(htmlText);
            writer.Close();
        }


        /// <summary>
        /// Use to get only the part that is inside the body tag from the html markup
        /// Useful if you need to embed this content into another html page
        /// </summary>
        /// <param name="htmlText">String with HTML text content</param>
        /// <returns>HTML body content as ref string</returns>
        private static void CleanHtmlContent(ref string htmlText)
        {
            string styles = GetFirstTagContent(htmlText, "style");
            string content = GetFirstTagContent(htmlText, "div", true);
            htmlText = $"<article>\n{styles}{content}</article>";
        }


        private static string GetFirstTagContent(string htmlText, string tagName, bool removeWrap = false)
        {
            int bodyOpenIndex = htmlText.IndexOf($"<{tagName}", StringComparison.Ordinal);
            int bodyCloseIndex = htmlText.IndexOf($"</{tagName}>", StringComparison.Ordinal);

            if (bodyOpenIndex == -1 || bodyCloseIndex == -1) return null;

            int before = removeWrap ? tagName.Length + 1 : 0;
            int after = removeWrap ? -3 : tagName.Length + 3;
            htmlText = htmlText.Substring(
                bodyOpenIndex + before,
                bodyCloseIndex - bodyOpenIndex + after);

            if (!removeWrap) return htmlText;

            int bodyOpenEndIndex = htmlText.IndexOf(">", StringComparison.Ordinal);
            int bodyCloseEndIndex = htmlText.LastIndexOf("<", StringComparison.Ordinal);

            if (bodyOpenEndIndex == -1 || bodyCloseEndIndex == -1) return null;

            htmlText = htmlText.Substring(
                bodyOpenEndIndex + 1,
                bodyCloseEndIndex - bodyOpenEndIndex - 1);

            return htmlText;
        }

        private static void RegexFixHtml(ref string htmlText)
        {
            htmlText = Regex.Replace(htmlText, "0pt", "0");
        }

        private static void RemoveTags(ref string htmlText, string tagName)
        {
            int bodyOpenIndex, bodyCloseIndex;

            while (true)
            {
                bodyOpenIndex = htmlText.IndexOf($"<{tagName}", StringComparison.Ordinal);
                bodyCloseIndex = htmlText.IndexOf($"</{tagName}>", StringComparison.Ordinal);

                if (bodyOpenIndex == -1 || bodyCloseIndex == -1) return;

                htmlText = htmlText.Remove(
                    bodyOpenIndex - 1,
                    bodyCloseIndex - bodyOpenIndex + tagName.Length + 4);
            }
        }
    }
}