using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessLayer.DataServices;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Apit.Controllers
{
    public partial class ArticlesController
    {
        [Authorize]
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
            if (!ModelState.IsValid) return View(model);

            // Combine all errors and return them back if this variable is set to true
            bool hasIncorrectData = false;

            #region ================ Long form review ================

            // Check selected topic existing
            var topic = model.TopicId == null ? null : _dataManager.Topics.GetById(Guid.Parse(model.TopicId));
            if (topic == null)
            {
                ModelState.AddModelError(nameof(model.TopicId),
                    "дана тема не може бути використана");
                hasIncorrectData = true;
            }

            // "hello, wor/*+#ld1!" != "hello, world1!"
            if (_keyWordsAvailableRegex.Replace(model.KeyWords, "") != "")
            {
                ModelState.AddModelError(nameof(model.KeyWords),
                    "Unsupported character detected");
                hasIncorrectData = true;
            }

            // Check uploaded file
            var extension = Path.GetExtension(model.DocFile.FileName);
            _logger.LogInformation("Upload file with extension: " + extension);
            if (model.DocFile.Length > 0)
            {
                if (!Regex.IsMatch(extension ?? "", @".docx?$"))
                {
                    ModelState.AddModelError(nameof(model.DocFile),
                        "невірний формат файлу (доступно лише .doc і .docx)");
                    hasIncorrectData = true;
                }
                else if (!hasIncorrectData)
                {
                    string err = await DataUtil.TrySaveDocFile(model.DocFile, model.UniqueAddress, extension);
                    if (err != null)
                    {
                        _logger.LogError("Document converter error\n" + err);
                        ModelState.AddModelError(nameof(model.DocFile),
                            "даний файл не може бути збереженим, оскільки може нести у собі загрозу для сервісу. " +
                            "Якщо це не так, будь ласка, зверніться до адміністрації сайту");
                        hasIncorrectData = true;
                    }
                }
            }
            else
            {
                ModelState.AddModelError(nameof(model.DocFile),
                    "будь ласка, прикрепіть файл з матеріалом");
                hasIncorrectData = true;
            }

            #endregion

            if (hasIncorrectData) return View(model);


            var dateNow = DateTime.Now;
            var user = await _userManager.GetUserAsync(User);

            var article = new Article
            {
                Id = Guid.NewGuid(),
                TopicId = topic.Id,
                CreatorId = user.Id,
                UniqueAddress = model.UniqueAddress,

                Title = model.Title,
                Status = ArticleStatus.Uploaded,
                KeyWords = _keyWordsSeparatorRegex.Replace(model.KeyWords, ";"),

                HtmlFilePath = model.UniqueAddress + ".htm",
                DocxFilePath = model.UniqueAddress + extension,

                Conference = _dataManager.Conferences.GetCurrentAsDbModel(),

                DateCreated = dateNow,
                DateLastModified = dateNow
            };

            var currentConf = _dataManager.Conferences.GetCurrentAsDbModel();
            _dataManager.Conferences.AddArticle(currentConf, article);
            _dataManager.Articles.Create(article);

            return RedirectToAction("index", "articles", new {id = model.UniqueAddress});
        }
    }
}