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
    public partial class AccountController : Controller // Maybe it is better to use the integrated Account ASP.NET functionality (Areas/Identity/Pages/Account(/Manage))
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataManager _dataManager;
        private readonly MailService _mailService;

        public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager,
            UserManager<User> userManager, DataManager dataManager, MailService mailService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _dataManager = dataManager;
            _mailService = mailService;
        }


        [Authorize]
        public async Task<IActionResult> Index(string x)
        {
            var user = x == null
                ? await _userManager.GetUserAsync(User)
                : _dataManager.Users.GetByUniqueAddress(x);

            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new EditUserViewModel {EmailConfirmed = user.EmailConfirmed});
        }

        [Route("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(new EditUserViewModel
                {
                    ResultMessage = "не вдалось дані змінити"
                });


            var user = await _userManager.GetUserAsync(User);

            model.FirstName = model.FirstName?.Trim();
            if (!string.IsNullOrWhiteSpace(model.FirstName))
                user.FirstName = model.FirstName;

            model.LastName = model.LastName?.Trim();
            if (!string.IsNullOrWhiteSpace(model.LastName))
                user.LastName = model.LastName;

            model.MiddleName = model.MiddleName?.Trim();
            if (!string.IsNullOrWhiteSpace(model.MiddleName))
                user.MiddleName = model.MiddleName;

            model.WorkingFor = model.WorkingFor?.Trim();
            if (!string.IsNullOrWhiteSpace(model.WorkingFor))
                user.WorkingFor = model.WorkingFor;

            if (!string.IsNullOrWhiteSpace(model.AltScienceDegree))
            {
                var str = model.ScienceDegree.ToString();
                if (user.ScienceDegree != str) user.ScienceDegree = str;
            }
            else if (user.ScienceDegree != model.AltScienceDegree)
                user.ScienceDegree = model.AltScienceDegree;


            if (!string.IsNullOrWhiteSpace(model.AltAcademicTitle))
            {
                var strAcad = model.AcademicTitle.ToString();
                if (user.AcademicTitle != strAcad) user.AcademicTitle = strAcad;
            }
            else if (user.AcademicTitle != model.AltAcademicTitle)
                user.AcademicTitle = model.AltAcademicTitle;

            if (user.ParticipationForm != model.ParticipationForm)
                user.ParticipationForm = model.ParticipationForm;


            //TODO: two-factor auth with phone number

            _dataManager.Users.SaveChanges();

            _logger.LogDebug($"User {user.ProfileAddress} has changed his data");

            return View(new EditUserViewModel {ResultMessage = "дані успішно змінено"});
        }
    }
}