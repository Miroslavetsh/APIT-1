using System.IO;
using BusinessLayer.DataServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Apit.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly ILogger<ResourcesController> _logger;

        public ResourcesController(ILogger<ResourcesController> logger)
        {
            _logger = logger;
        }


        public IActionResult Document(string src)
        {
            var data = DataUtil.LoadDocFile(src);
            if (data == null) return View("error");
            return PhysicalFile(data.FilePath, data.MimeType, data.FileName);
        }

        public VirtualFileResult Static(string src)
        {
            if (src != "article-example.docx")
            {
                Response.StatusCode = 404;
                return null;
            }
            
            var filepath = Path.Combine("~/Resources", src);
            return File(filepath, MIME.DOCX, src);
        }
    }
}