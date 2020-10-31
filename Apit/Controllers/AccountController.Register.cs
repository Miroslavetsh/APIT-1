using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Apit.Controllers
{
    public partial class AccountController // Maybe it is better to use the integrated Account ASP.NET functionality (Areas/Identity/Pages/Account(/Manage))
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
                    var roleResult = await _userManager.AddToRoleAsync(user, "superman");

                    if (roleResult.Succeeded)
                    {
                        roleResult = await _userManager.AddToRoleAsync(user, "organizer");

                        if (!roleResult.Succeeded)
                            _logger.LogInformation("First user authorized as admin with full access");
                    }
                    else ModelState.AddModelError(string.Empty, "You are not admin!");
                }

                string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

                string confirmationLink = Url.Action
                ("ConfirmEmail", "account", new
                {
                    id = user.Id,
                    token = confirmationToken
                }, protocol: HttpContext.Request.Scheme);

                _mailService.SendEmail(user.Email,
                    "Confirm your email | Підтвердіть Вашу пошту",
                    confirmationLink);
                _logger.LogDebug("Confirmation email was sent to: " + user.Email);

                await _signInManager.SignInAsync(user, false);
                _logger.LogDebug($"User {user.ProfileAddress} has successfully registered");
                return LocalRedirect(model.ReturnUrl ?? "/");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }


        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string id, string token)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(token))
                return View("Error");

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                _logger.LogDebug($"User {user.ProfileAddress} confirmed mail");
                ViewBag.Message = "Email confirmed successfully!";
                return View("Success");
            }

            _logger.LogError($"User {user.ProfileAddress} NOT confirmed mail");
            ViewBag.Message = "Error while confirming your email!";
            return View("Error");
        }
    }
}