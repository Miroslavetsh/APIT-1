using System;
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

        void AddParticipant(User user);
        void AddAdmin(User user);
        void AddArticle(Article article);
        void AddImage(IFormFile image);
    }
}