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
    public partial class ConferenceRepository : IConferencesRepository
    {
        private readonly AppDbContext _ctx;
        private readonly UsersRepository _users;
        private readonly ArticlesRepository _articles;

        public ConferenceRepository(AppDbContext context,
            UsersRepository usersRepo, ArticlesRepository articlesRepo)
        {
            _ctx = context;
            _users = usersRepo;
            _articles = articlesRepo;
        }

        public string GenerateUniqueAddress() => DataUtil.GenerateUniqueAddress(this, 8);

        public ConferenceViewModel GetById(Guid id) =>
            ConvertToViewModel(_ctx.Conferences.FirstOrDefault(conf => conf.Id == id));

        public IEnumerable<ConferenceViewModel> GetAll() => ConvertToViewModel(_ctx.Conferences);

        public void SaveChanges() => _ctx.SaveChanges();

        public bool IsExist(Guid id) => _ctx.Conferences.Any(conf => conf.Id == id);


        public ConferenceViewModel GetByUniqueAddress(string address) =>
            ConvertToViewModel(_ctx.Conferences.FirstOrDefault(a => a.UniqueAddress == address));

        public IEnumerable<ConferenceViewModel> GetLatest(ushort count) =>
            ConvertToViewModel(_ctx.Conferences.OrderByDescending(a => a.DateCreated).Take(count));


        public void Create(ConferenceViewModel entity)
        {
            if (entity == null) throw new ArgumentNullException();

            var instance = _ctx.Conferences.FirstOrDefault(a => a.Id == entity.Id);

            if (instance != null)
            {
                ConvertViewModelToDbObject(entity, instance);
            }
            else
            {
                var newInstance = new Conference();
                ConvertViewModelToDbObject(entity, newInstance);
                _ctx.Conferences.Add(newInstance);
            }

            SaveChanges();
        }

        public void Delete(Guid id)
        {
            _ctx.Conferences.Remove(new Conference {Id = id});
            SaveChanges();
        }


        private ConferenceViewModel ConvertToViewModel(Conference conf)
        {
            return new ConferenceViewModel
            {
                Id = conf.Id,
                Title = conf.Title,
                ShortDescription = conf.ShortDescription,
                Description = conf.Description,

                Participants = GetConfParticipants(conf),
                Admins = GetConfAdmins(conf),
                Articles = GetConfArticles(conf),

                DateCreated = conf.DateCreated,
                DateLastModified = conf.DateLastModified,
                DateStart = conf.DateStart,
                Duration = conf.Duration
            };
        }

        private IEnumerable<ConferenceViewModel> ConvertToViewModel
            (IEnumerable<Conference> conf) => conf.Select(ConvertToViewModel);

        private void ConvertViewModelToDbObject(ConferenceViewModel model, Conference instance)
        {
            instance.Id = model.Id;
            instance.UniqueAddress = model.UniqueAddress;
            instance.Title = model.Title;

            instance.ShortDescription = model.ShortDescription;
            instance.Description = model.Description;

            instance.Participants = model.Participants.Select(a => new ConferenceParticipant {Id = a.Id}).ToList();
            instance.Admins = model.Admins.Select(a => new ConferenceAdmin {Id = a.Id}).ToList();
            instance.Articles = _articles.GetBtConference(instance).ToList();

            instance.DateCreated = model.DateCreated;
            instance.DateLastModified = model.DateLastModified;
            instance.DateStart = model.DateStart;
            instance.Duration = model.Duration;
        }
    }
}