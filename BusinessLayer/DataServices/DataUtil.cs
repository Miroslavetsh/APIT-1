using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Words;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using SautinSoft;


namespace BusinessLayer.DataServices
{
    public static class DataUtil
    {
        public const string DOCS_DIR = "../Local/ArticlesDocs/";
        public const string HTML_DIR = "../Local/ArticlesHtml/";
        public const string IMAGES_DIR = "../Local/Gallery/";


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


        public static async Task<string> TrySaveDocFile(IFormFile docFile, string fileName, string extension)
        {
            try
            {
                var docxPath = Path.Combine(DOCS_DIR, fileName + extension);
                var htmlPath = Path.Combine(HTML_DIR, fileName + Extension.Htm);

                await using var stream = new FileStream(docxPath, FileMode.Create);
                await docFile.CopyToAsync(stream);
                stream.Close();

                await MSWordParser.Save(docxPath, htmlPath, RtfToHtml.eOutputFormat.HTML_5);

                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
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


        public static PhysicalFileData GetLoadDocFileOptions(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), DOCS_DIR, fileName);
            if (!File.Exists(filePath)) return null;

            string mimeType;
            switch (Path.GetExtension(fileName))
            {
                case Extension.Doc:
                    mimeType = MIME.DOC;
                    break;
                case Extension.Docx:
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