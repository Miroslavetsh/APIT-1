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


        public IActionResult Document(string id)
        {
            return LocalRedirect("/");
        }
    }
}