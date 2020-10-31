using System.Diagnostics;
using System.Threading.Tasks;
using Apit.Service;
using BusinessLayer;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Apit.Controllers
{
    [Authorize(Roles = "organizer")]
    public partial class ConferenceController : Controller
    {
        private readonly ILogger<ConferenceController> _logger;
        private readonly DataManager _dataManager;
        private readonly UserManager<User> _userManager;
        private readonly ProjectConfig.ContentDataConfig _config;

        public ConferenceController(ILogger<ConferenceController> logger,
            DataManager dataManager, UserManager<User> userManager, ProjectConfig config)
        {
            _logger = logger;
            _dataManager = dataManager;
            _userManager = userManager;
            _config = config.Content.Conference;
        }


        [AllowAnonymous]
        public IActionResult Index(string id)
        {
            var current = string.IsNullOrWhiteSpace(id)
                ? _dataManager.Conferences.GetCurrent()
                : _dataManager.Conferences.GetByUniqueAddress(id);

            return View(current);
        }


        [AllowAnonymous, Route("join-now")]
        public async Task<IActionResult> JoinNow()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("register", "account");

            var user = await _userManager.GetUserAsync(User);
            var conference = _dataManager.Conferences.GetCurrentAsDbModel();

            _dataManager.Conferences.AddParticipant(conference, user);
            ViewBag.ResultMessage = "<span>Добро пожаловать!</span>";
            return View("index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Unsubscribe()
        {
            var user = await _userManager.GetUserAsync(User);
            var conference = _dataManager.Conferences.GetCurrentAsDbModel();

            _dataManager.Conferences.AddParticipant(conference, user);
            ViewBag.ResultMessage = "<span>Ви больше не с нами</span>";
            return View("index");
        }

        [AllowAnonymous]
        public IActionResult Archive(int page = 1)
        {
            return View();
        }

        [Route("move-to-archive")]
        public IActionResult MoveToArchive()
        {
            var current = _dataManager.Conferences.GetCurrent();
            current.IsActual = false;
            _dataManager.Conferences.SaveChanges();

            _logger.LogInformation($"Conference {current.UniqueAddress} archived");

            return RedirectToAction("archive", "conference");
        }

        [AllowAnonymous, ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}