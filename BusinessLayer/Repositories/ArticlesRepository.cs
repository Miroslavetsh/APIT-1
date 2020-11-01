using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ConvertToViewModel(_ctx.Articles.FirstOrDefault(a => a.Id == id)).Result;

        public bool IsExist(Guid id) => _ctx.Articles.Any(a => a.Id == id);

        public ArticleViewModel GetByUniqueAddress(string address) =>
            ConvertToViewModel(_ctx.Articles.FirstOrDefault(a => a.UniqueAddress == address)).Result;

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

        // What happens in the case when one article has many authors and vice versa?
        public IEnumerable<ArticleViewModel> GetByCreator(string userId) =>
            ConvertToViewModel(_ctx.Articles.Where(a => a.CreatorId == userId));

        public IEnumerable<Article> GetByConference(Conference conf) =>
            _ctx.Articles.Where(a => a.Conference == conf);

        public void SaveChanges() => _ctx.SaveChanges();


        public void Create(Article entity)
        {
            if (entity == null) throw new ArgumentNullException();

            _ctx.Articles.Add(entity);

            SaveChanges();
        }

        public void Delete(Guid id)
        {
            _ctx.Articles.Remove(_ctx.Articles.First(a => a.Id == id));
            SaveChanges();
        }

        // It will be more convenient to do this using specific constructor
        private async Task<ArticleViewModel> ConvertToViewModel(Article article)
        {
            if (article == null) return null;

            return new ArticleViewModel
            {
                Id = article.Id,
                Topic = _ctx.Topics.FirstOrDefault(t => t.Id == article.TopicId),
                Creator = _ctx.Users.FirstOrDefault(u => u.Id == article.CreatorId),

                UniqueAddress = article.UniqueAddress,
                DocFileAddress = article.DocxFilePath,
                HTMLContent = await DataUtil.LoadHtmlFile(article.HtmlFilePath),

                Title = article.Title,
                Status = article.Status,
                KeyWords = article.KeyWords.Split(';'),

                DateCreated = article.DateCreated,
                DateLastModified = article.DateLastModified
            };
        }

        private IEnumerable<ArticleViewModel> ConvertToViewModel(IEnumerable<Article> articles) =>
            articles.Select(article => ConvertToViewModel(article).Result);
    }
}