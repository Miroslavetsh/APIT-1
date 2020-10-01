using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IArticlesRepository : ICollectedData<Guid, ArticleViewModel>
    {
        IEnumerable<ArticleViewModel> GetByCodeWord(string word);
        IEnumerable<ArticleViewModel> GetLatest(ushort count);
        IEnumerable<ArticleViewModel> GetByUser(string userId);
    }
}