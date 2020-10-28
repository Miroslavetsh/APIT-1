using System;
using System.Threading.Tasks;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                ScienceDegree = model.ScienceDegree,
                AcademicTitle = model.AcademicTitle,
                ParticipationForm = model.ParticipationForm,

                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(model.ReturnUrl ?? "/");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
    }
}