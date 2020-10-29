using System;
using System.Threading.Tasks;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apit.Controllers
{
    public partial class ConferenceController
    {
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> Create(NewConferenceViewModel model)
        {
            // TODO: new conference validation


            _dataManager.Conferences.Create(model);

            return View(model);
        }

        [Authorize]
        public IActionResult Update()
        {
            return View();
        }

        [Authorize, HttpPost]
        public IActionResult Delete()
        {
            return RedirectToAction("index", "conference");
        }
    }
}