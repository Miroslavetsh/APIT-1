using System;
using System.Threading.Tasks;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apit.Controllers
{
    public partial class ArticlesController
    {
        public IActionResult Create()
        {
            return View(new NewArticleViewModel
            {
                UniqueAddress = _dataManager.Articles.GenerateUniqueAddress()
            });
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(NewArticleViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var dateNow = DateTime.Now;

            // Get topic
            var topic = model.CreateNewTopic
                        && !string.IsNullOrWhiteSpace(model.NewTopicName)
                ? _dataManager.Topics.IsExist(model.NewTopicName)
                    ? _dataManager.Topics.GetByName(model.NewTopicName)
                    : new Topic {Id = Guid.NewGuid(), Name = model.NewTopicName}
                : _dataManager.Topics.GetById(Guid.Parse(model.TopicId));

            if (topic == null)
            {
                ModelState.AddModelError(
                    nameof(NewArticleViewModel.NewTopicName),
                    "Topic not defined");
                return View(model);
            }

            // Add topic to DB if it is new one
            if (!_dataManager.Topics.IsExist(topic.Id))
                _dataManager.Topics.Create(topic);

            if (_dataManager.Conferences.GetByUniqueAddress(model.UniqueAddress) == null)
            {
                ModelState.AddModelError(
                    nameof(NewArticleViewModel.UniqueAddress),
                    "This address already used");
                return View(model);
            }

            if (model.UseFromFile)
            {
                Console.WriteLine("======= UseFromFile IS TRUE =======");
                throw new NotImplementedException();
                //TODO: Create article by MS Word Document uploaded file by user
            }
            else
            {
                _dataManager.Articles.Create(new ArticleViewModel
                {
                    Id = Guid.NewGuid(),
                    UniqueAddress = model.UniqueAddress,
                    Topic = topic,
                    Creator = user,

                    DateCreated = dateNow,
                    DateLastModified = dateNow,
                    KeyWords = model.KeyWords.Split(' ', ',', ';'),

                    Title = model.Title,
                    HTML = string.IsNullOrWhiteSpace(model.TextHTML) ? " ==== Empty ====" : model.TextHTML
                });
            }

            return Redirect("/articles/list");
        }

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
                var articleId = Guid.Parse(id);
                if (_dataManager.Articles.IsExist(articleId))
                {
                    var article = _dataManager.Articles.GetById(articleId);
                    var user = await _userManager.GetUserAsync(User);

                    if (user == article.Creator) _dataManager.Articles.Delete(articleId);
                    else ModelState.AddModelError(nameof(ArticleViewModel.Creator), "User access denied");
                }
                else
                    ModelState.AddModelError(nameof(ArticleViewModel.Id), "Article not exist");

                return Redirect(returnUrl ?? "/");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Error();
            }
        }
    }
}