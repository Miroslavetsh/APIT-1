using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apit.Service;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Apit.Controllers
{
    public partial class ConferenceController
    {
        public IActionResult Create()
        {
            var dateNow = DateTime.Now;
            var model = new NewConferenceViewModel
            {
                UniqueAddress = _dataManager.Conferences.GenerateUniqueAddress(),
                DateStart = dateNow,
                DateFinish = dateNow
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewConferenceViewModel model)
        {
            // if (!ModelState.IsValid) return View(model);

            // Combine all errors and return them back if this variable is set to true
            bool hasIncorrectData = false;

            #region ================ Long form review ================

            // Check unique address field value
            model.UniqueAddress.NormalizeAddress();
            if (model.UniqueAddress.Length < 5)
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.UniqueAddress),
                    "адреса закоротка (потрібна довжина 5-25 символів)");
                hasIncorrectData = true;
            }
            else if (model.UniqueAddress.Length > 25)
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.UniqueAddress),
                    "адреса задовга (потрібна довжина 5-20 символів)");
                hasIncorrectData = true;
            }
            else if (_dataManager.Articles.GetByUniqueAddress(model.UniqueAddress) != null)
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.UniqueAddress),
                    "ця адреса вже використовується, оберіть іншу");
                hasIncorrectData = true;
            }


            if (string.IsNullOrWhiteSpace(model.ShortDescription))
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.ShortDescription),
                    "введіть скорочений опис");
                hasIncorrectData = true;
            }

            if (string.IsNullOrWhiteSpace(model.Description))
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.Description),
                    "введіть опис");
                hasIncorrectData = true;
            }

            if (model.Topics.All(item => item == null))
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.Topics),
                    "задайте як мінімум одну тему");
                hasIncorrectData = true;
            }

            var user = await _userManager.GetUserAsync(User);
            var adminKeys = model.AdminKeys.Distinct();
            model.Admins = new List<ConferenceAdmin>();
            model.Admins.Add(new ConferenceAdmin
            {
                Id = Guid.NewGuid(),
                UserId = user.Id
            });

            foreach (var adminKey in adminKeys)
            {
                if (string.IsNullOrWhiteSpace(adminKey)) continue;
                if (adminKey == user.ProfileAddress)
                {
                    ModelState.AddModelError(nameof(NewConferenceViewModel.AdminKeys),
                        "ви автоматично матимете повний доступ");
                    continue;
                }

                var admin = _dataManager.Users.GetByUniqueAddress(adminKey);
                if (admin == null)
                {
                    ModelState.AddModelError(nameof(NewConferenceViewModel.AdminKeys),
                        $"користувача {adminKey} не існує");
                    hasIncorrectData = true;
                }
                else
                {
                    model.Admins.Add(new ConferenceAdmin
                    {
                        Id = Guid.NewGuid(),
                        UserId = admin.Id
                    });
                }
            }

            if (model.Admins.All(item => item == null))
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.AdminKeys),
                    "задайте як мінімум одного адманістратора");
                hasIncorrectData = true;
            }

            if (model.DateStart < DateTime.Today)
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.DateStart),
                    "невірно задано дату");
                hasIncorrectData = true;
            }

            if (model.DateFinish < DateTime.Today)
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.DateFinish),
                    "невірно задано дату");
                hasIncorrectData = true;
            }

            if (model.DateStart > model.DateFinish)
            {
                ModelState.AddModelError(nameof(NewConferenceViewModel.DateFinish),
                    "невірно задано дати");
                hasIncorrectData = true;
            }

            #endregion

            if (hasIncorrectData) return View(model);

            _dataManager.Conferences.Create(model);
            _logger.LogInformation($"New conference {model.UniqueAddress} created");

            return RedirectToAction("index", "conference");
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete()
        {
            return RedirectToAction("index", "conference");
        }
    }
}