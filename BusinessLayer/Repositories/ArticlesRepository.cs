using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.DataServices;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DatabaseLayer;
using DatabaseLayer.Entities;

namespace BusinessLayer.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly AppDbContext _ctx;

        public ArticlesRepository(AppDbContext context)
        {
            _ctx = context;
        }

        public string GenerateUniqueAddress() => DataUtil.GenerateUniqueAddress(this, 8);

        public IEnumerable<ArticleViewModel> GetAll() => ConvertToViewModel(_ctx.Articles);

        public ArticleViewModel GetById(Guid id) =>
            ConvertToViewModel(_ctx.Articles.FirstOrDefault(a => a.Id == id));

        public bool IsExist(Guid id) => _ctx.Articles.Any(a => a.Id == id);

        public ArticleViewModel GetByUniqueAddress(string address) =>
            ConvertToViewModel(_ctx.Articles.FirstOrDefault(a => a.UniqueAddress == address));

        public IEnumerable<ArticleViewModel> GetByCodeWord(string word)
        {
            bool SelectorFunction(Article a)
            {
                var keys = a.KeyWords.Split(' ');
                return keys.Any(kw => kw == word);
            }

            return ConvertToViewModel(_ctx.Articles.Where(SelectorFunction));
        }


        public IEnumerable<ArticleViewModel> GetLatest(ushort count) =>
            ConvertToViewModel(_ctx.Articles.OrderByDescending(a => a.DateCreated).Take(count));

        public IEnumerable<ArticleViewModel> GetByCreator(string userId) =>
            ConvertToViewModel(_ctx.Articles.Where(a => a.CreatorId == userId));

        public IEnumerable<Article> GetBtConference(Conference conf) => 
            _ctx.Articles.Where(a => a.Conference == conf);

        public void SaveChanges() => _ctx.SaveChanges();

        public void Create(ArticleViewModel entity)
        {
            if (entity == null) throw new ArgumentNullException();

            var instance = _ctx.Articles.FirstOrDefault(a => a.Id == entity.Id);

            if (instance != null)
            {
                ConvertViewModelToDbObject(entity, instance);
            }
            else
            {
                var newInstance = new Article();
                ConvertViewModelToDbObject(entity, newInstance);
                _ctx.Articles.Add(newInstance);
            }

            SaveChanges();
        }

        public void Delete(Guid id)
        {
            _ctx.Articles.Remove(_ctx.Articles.First(a => a.Id == id));
            SaveChanges();
        }


        private ArticleViewModel ConvertToViewModel(Article article)
        {
            if (article == null) return null;

            return new ArticleViewModel
            {
                Id = article.Id,
                UniqueAddress = article.UniqueAddress,
                Topic = _ctx.Topics.FirstOrDefault(t => t.Id == article.TopicId),
                Creator = _ctx.Users.FirstOrDefault(u => u.Id == article.CreatorId),

                Title = article.Title,
                Status = article.Status,
                KeyWords = article.KeyWords.Split(' '),
                HTML = DataUtil.LoadArticle(article.DataFile),

                DateCreated = article.DateCreated,
                DateLastModified = article.DateLastModified
            };
        }

        private IEnumerable<ArticleViewModel> ConvertToViewModel
            (IEnumerable<Article> articles) => articles.Select(ConvertToViewModel);

        private static void ConvertViewModelToDbObject(ArticleViewModel model, Article instance)
        {
            instance.Id = model.Id;
            instance.TopicId = model.Topic.Id;
            instance.CreatorId = model.Creator.Id;

            instance.Title = model.Title;
            instance.Status = model.Status;
            instance.KeyWords = string.Join(' ', model.KeyWords);
            //TODO: Loading article content from the file system :>
            instance.DataFile = model.HTML;

            instance.DateCreated = model.DateCreated;
            instance.DateLastModified = model.DateLastModified;
        }
    }
}