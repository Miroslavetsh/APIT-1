using System;
using System.Threading.Tasks;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Apit.Controllers
{
    public partial class ArticlesController
    {
        [Authorize]
        public IActionResult Edit(string id)
        {
            var model = _dataManager.Articles.GetById(Guid.Parse(id));
            return View(model);
        }

        [HttpPost, Authorize]
        public IActionResult Edit(ArticleViewModel model)
        {
            //TODO: Edit page response 
            // var user = await _userManager.GetUserAsync(User);
            // if (model.Creator != user)
            //     ModelState.AddModelError(nameof(ArticleViewModel.Creator),
            //         "User access denied");

            return View();
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Delete(string id, string returnUrl = null)
        {
            try
            {
                var article = _dataManager.Articles.GetByUniqueAddress(id);
                var user = await _userManager.GetUserAsync(User);

                if (user == article.Creator) _dataManager.Articles.Delete(article.Id);
                else ModelState.AddModelError(nameof(ArticleViewModel.Creator), "доступ заблоковано");

                return LocalRedirect(returnUrl ?? "/");
            }
            catch (Exception e)
            {
                _logger.LogError("Article not deleted with exception: " + e);
                return Error();
            }
        }
    }
}