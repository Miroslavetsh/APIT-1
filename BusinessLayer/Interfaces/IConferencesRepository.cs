using System;
using System.Collections.Generic;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Interfaces
{
    public interface IConferencesRepository :
        ICollectedData<Guid, ConferenceViewModel, Conference>,
        IAddressedData<ConferenceViewModel>
    {
        ConferenceViewModel GetCurrent();
        Conference GetCurrentAsDbModel();
        void Update(ConferenceViewModel model);

        IEnumerable<User> GetConfParticipants(Conference conference);
        IEnumerable<User> GetConfAdmins(Conference conference);
        IEnumerable<ArticleViewModel> GetConfArticles(Conference conference);
        IEnumerable<string> GetConfImages(Conference conference);
        
        void AddParticipant(User user);
        void AddAdmin(User user);
        void AddArticle(Article article);
        void AddImage(IFormFile image);
    }
}