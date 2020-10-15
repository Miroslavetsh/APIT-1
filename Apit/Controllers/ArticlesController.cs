using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Models;
using BusinessLayer;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace Apit.Controllers
{
    public partial class ArticlesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly DataManager _dataManager;


        public ArticlesController(UserManager<User> userManager, DataManager dataManager)
        {
            _userManager = userManager;
            _dataManager = dataManager;
        }


        public IActionResult P(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) Error();
            Console.WriteLine("ID: " + id);
            var article = _dataManager.Articles.GetByUniqueAddress(id);
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
                    model.Collection = _dataManager.Articles.GetByCreator(user.Id)
                        .OrderBy(a => a.DateLastModified).Reverse();
                    break;
                }
                default:
                {
                    model.Filter = "all";
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