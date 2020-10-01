using System;
using Microsoft.AspNetCore.Mvc;

namespace Apit.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("Admin logged in. <========================================>");
            return View();
        }
    }
}