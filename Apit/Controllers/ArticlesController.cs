using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessLayer.Models;
using BusinessLayer;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Apit.Controllers
{
    public partial class ArticlesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly DataManager _dataManager;


        public ArticlesController(ILogger<HomeController> logger,
            UserManager<User> userManager, DataManager dataManager)
        {
            _logger = logger;
            _userManager = userManager;
            _dataManager = dataManager;
        }


        public IActionResult P(string id = "", string returnUrl = "")
        {
            var guidId = Guid.Parse(id);

            var article = string.IsNullOrWhiteSpace(id)
                ? _dataManager.Articles.GetLatest(1).FirstOrDefault()
                : _dataManager.Articles.GetById(guidId);

            return article == null ? Error() : View(article);
        }

        public async Task<IActionResult> List(ArticlesListViewModel model)
        {
            ViewData["Title"] = "Articles page title";

            switch (model.Filter)
            {
                case "my":
                {
                    var user = await _userManager.GetUserAsync(User);
                    model.Collection = _dataManager.Articles.GetByUser(user.Id)
                        .OrderBy(a => a.DateLastModified).Reverse();
                    break;
                }
                default:
                {
                    model.Collection = _dataManager.Articles.GetAll()
                        .OrderBy(a => a.DateLastModified).Reverse();
                    break;
                }
            }

            return View(model);
        }

        public IActionResult MyArticles()
        {
            return Redirect("list?filter=my");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}