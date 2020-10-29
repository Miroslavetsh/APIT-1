using System.Diagnostics;
using BusinessLayer;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Apit.Controllers
{
    public partial class ConferenceController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<User> _userManager;

        public ConferenceController(DataManager dataManager, UserManager<User> userManager)
        {
            _dataManager = dataManager;
            _userManager = userManager;
        }

        public IActionResult Index(string id)
        {
            var current = string.IsNullOrWhiteSpace(id)
                ? _dataManager.Conferences.GetCurrent()
                : _dataManager.Conferences.GetByUniqueAddress(id);

            return View(current);
        }

        public IActionResult Archive(int page = 1)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}