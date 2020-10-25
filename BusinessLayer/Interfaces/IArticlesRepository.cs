using System;
using System.Collections.Generic;
using BusinessLayer.Models;
using DatabaseLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IArticlesRepository :
        ICollectedData<Guid, ArticleViewModel, Article>,
        IAddressedData<ArticleViewModel>
    {
        IEnumerable<ArticleViewModel> GetByCodeWord(string word);
        IEnumerable<ArticleViewModel> GetByCreator(string userId);
        IEnumerable<Article> GetByConference(Conference conf);
    }
}