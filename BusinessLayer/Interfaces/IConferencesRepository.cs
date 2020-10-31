using System;
using System.Collections.Generic;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Interfaces
{
    public interface IConferencesRepository :
        ICollectedData<Guid, ConferenceViewModel, NewConferenceViewModel>,
        IAddressedData<ConferenceViewModel>
    {
        ConferenceViewModel GetCurrent();
        Conference GetCurrentAsDbModel();
        void Update(ConferenceViewModel model);

        IEnumerable<User> GetConfParticipants(Conference conference);
        IEnumerable<User> GetConfAdmins(Conference conference);
        IEnumerable<ArticleViewModel> GetConfArticles(Conference conference);
        IEnumerable<string> GetConfImages(Conference conference);

        void AddParticipant(Conference conference, User user);
        void AddAdmin(Conference conference, User user);
        void AddArticle(Conference conference, Article article);
        void AddImage(Conference conference, IFormFile image);

        void RemoveParticipant(Conference conference, User user);
        void RemoveAdmin(Conference conference, User user);
        void RemoveArticle(Conference conference, Article article);
        void RemoveImage(Conference conference, string path);
    }
}