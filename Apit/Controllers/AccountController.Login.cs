using System.Threading.Tasks;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Apit.Controllers
{
    public partial class AccountController
    {
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel {ReturnUrl = returnUrl});
        }

        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _signInManager.PasswordSignInAsync
                (model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                _logger.LogDebug($"User {user.ProfileAddress} has successfully logged in");
                return LocalRedirect(model.ReturnUrl ?? "/");
            }

            ModelState.AddModelError(string.Empty, "невірно введено дані");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            _logger.LogDebug("User logged out");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}