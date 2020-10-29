using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Apit.Controllers
{
    public partial class AccountController
    {
        public IActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new RegisterViewModel {ReturnUrl = returnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                ProfileAddress = _dataManager.Users.GenerateUniqueAddress(),

                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,

                WorkingFor = model.WorkingFor,
                ScienceDegree = model.ScienceDegree.ToString(),
                AcademicTitle = model.AcademicTitle.ToString(),
                ParticipationForm = model.ParticipationForm,

                Email = model.Email,
                UserName = model.Email
            };

            bool isFirst = !_userManager.Users.Any();

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (isFirst)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "admin");
                    if (!roleResult.Succeeded)
                        ModelState.AddModelError(string.Empty, "You are not admin!");
                }

                string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

                string confirmationLink = Url.Action
                ("confirmemail", "account", new
                {
                    id = user.Id,
                    token = confirmationToken
                }, protocol: HttpContext.Request.Scheme);

                _mailService.SendEmail(user.Email,
                    "Confirm your email | Підтвердіть Вашу пошту",
                    confirmationLink);

                await _signInManager.SignInAsync(user, false);
                _logger.LogDebug($"User {user.ProfileAddress} has successfully registered");
                return LocalRedirect(model.ReturnUrl ?? "/");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }


        public async Task<IActionResult> ConfirmEmail(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                _logger.LogDebug($"User {user.ProfileAddress} confirmed his mail");
                ViewBag.Message = "Email confirmed successfully!";
                return View("Success");
            }

            ViewBag.Message = "Error while confirming your email!";
            return View("Error");
        }
    }
}