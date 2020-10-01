using System;
using System.Threading.Tasks;
using BusinessLayer.Models;
using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apit.Controllers
{
    [Authorize]
    public partial class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var isSuccess = true;
            var errorMessage = "error";
            var successMessage = "success";

            if (!ModelState.IsValid)
            {
                model.ResultMessage = errorMessage;
                return View(model);
            }

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

            if (!string.IsNullOrWhiteSpace(model.ScienceDegree))
            {
                if (Enum.TryParse<ScienceDegree>(model.ScienceDegree, out var scienceDegree))
                {
                    if (user.ScienceDegree != scienceDegree) user.ScienceDegree = scienceDegree;
                }
                else isSuccess = false;
            }

            if (!string.IsNullOrWhiteSpace(model.AcademicTitle))
            {
                if (Enum.TryParse<AcademicTitle>(model.AcademicTitle, out var academicTitle))
                {
                    if (user.AcademicTitle != academicTitle) user.AcademicTitle = academicTitle;
                }
                else isSuccess = false;
            }

            if (!string.IsNullOrWhiteSpace(model.ParticipationForm))
            {
                if (Enum.TryParse<ParticipationForm>(model.ParticipationForm, out var participationForm))
                {
                    if (user.ParticipationForm != participationForm) user.ParticipationForm = participationForm;
                }
                else isSuccess = false;
            }


            //TODO: tro factor auth with phone number

            _dataManager.Users.SaveChanges();

            return View(new EditUserViewModel {ResultMessage = isSuccess ? successMessage : errorMessage});
        }
    }
}