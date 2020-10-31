using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Models;
using BusinessLayer;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Apit.Controllers
{
    public partial class ArticlesController : Controller
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly DataManager _dataManager;

        private readonly Regex _keyWordsAvailableRegex;
        private readonly Regex _keyWordsSeparatorRegex;

        public ArticlesController(ILogger<ArticlesController> logger,
            UserManager<User> userManager, DataManager dataManager)
        {
            _logger = logger;
            _userManager = userManager;
            _dataManager = dataManager;

            _keyWordsAvailableRegex = new Regex("^[а-яА-Яa-zA-Z0-9 ,;'іїєґ]+$", RegexOptions.Compiled);
            _keyWordsSeparatorRegex = new Regex("[ ,;]", RegexOptions.Compiled);
        }


        public IActionResult Index(string id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("login", "account");

            if (string.IsNullOrWhiteSpace(id)) Error();
            var article = _dataManager.Articles.GetByUniqueAddress(id);
            return article == null ? Error() : View(article);
        }

        public IActionResult List(ArticlesListViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("login", "account");

            ViewData["Title"] = "Articles page title";

            switch (model.Filter)
            {
                case "all":
                {
                    model.Collection = _dataManager.Articles.GetAll()
                        .OrderBy(a => a.DateLastModified).Reverse();
                    break;
                }
                default:
                {
                    model.Filter = "current";
                    model.Collection = _dataManager.Conferences.GetCurrent().Articles
                        .OrderBy(a => a.DateLastModified).Reverse();
                    break;
                }
            }

            return View(model);
        }

        public IActionResult Example()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("login", "account");
            
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}