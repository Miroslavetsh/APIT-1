using System;
using System.IO;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.DataServices
{
    public static class DataUtil
    {
        public const string DOCS_DIR = "../../Local/Docs/";
        public const string HTML_DIR = "../../Local/Html/";
        public const string IMAGES_DIR = "../../Local/Gallery/";

        public static string GenerateUniqueAddress<TKey>(IAddressedData<TKey> data, int length)
        {
            int iter = 0;
            string address;
            do
            {
                var guid = Guid.NewGuid().ToString("N");
                address = guid.Substring(guid.Length - length);
            } while (data.GetByUniqueAddress(address) != null && iter++ < 500);

            return iter >= 500 ? null : address;
        }

        public static async Task<bool> TrySaveDocFile(IFormFile docFile, string fileName, string extension)
        {
            var docxPath = Path.Combine(DOCS_DIR, fileName + "." + extension);
            var htmlPath = Path.Combine(HTML_DIR, fileName + ".htm");

            await using var stream = new FileStream(docxPath, FileMode.Create);
            await docFile.CopyToAsync(stream);

            MSWordParser.SaveAsHtml(docxPath, htmlPath);
            return true;
        }

        public static async void SaveFile(IFormFile sourceFile, string fileName)
        {
            await using var file = new FileStream(fileName, FileMode.Create);
            await sourceFile.CopyToAsync(file);
        }

        internal static async Task<string> LoadHtmlFile(string fileName)
        {
            var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), HTML_DIR, fileName);
            using var file = new StreamReader(htmlPath);
            return await file.ReadToEndAsync();
        }

        public static PhysicalFileData LoadDocFile(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), DOCS_DIR, fileName);
            if (!File.Exists(filePath)) return null;

            string mimeType;
            switch (Path.GetExtension(fileName))
            {
                case "doc":
                    mimeType = MIME.DOC;
                    break;
                case "docx":
                    mimeType = MIME.DOCX;
                    break;
                default:
                    return null;
            }

            return new PhysicalFileData
            {
                FilePath = filePath,
                MimeType = mimeType,
                FileName = fileName
            };
        }
    }
}