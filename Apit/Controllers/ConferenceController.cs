using System.Diagnostics;
using BusinessLayer;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Apit.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly DataManager _dataManager;

        public ConferenceController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) Error();
            var current = _dataManager.Conferences.GetByUniqueAddress(id);
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