using Microsoft.AspNetCore.Mvc;

namespace Apit.Controllers
{
    public class ResourcesController : Controller
    {
        public IActionResult Get(string id)
        {
            return Redirect("/");
        }
    }
}