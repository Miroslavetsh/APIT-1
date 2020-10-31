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
        private readonly IUsersRepository _users;
        private readonly IArticlesRepository _articles;

        public ConferenceRepository(AppDbContext context,
            IUsersRepository usersRepo, IArticlesRepository articlesRepo)
        {
            _ctx = context;
            _users = usersRepo;
            _articles = articlesRepo;
        }

        public string GenerateUniqueAddress() => DataUtil.GenerateUniqueAddress(this, 8);


        public ConferenceViewModel GetCurrent() => ConvertToViewModel(GetCurrentAsDbModel());
        public Conference GetCurrentAsDbModel() => _ctx.Conferences.FirstOrDefault(a => a.IsActual);


        public ConferenceViewModel GetById(Guid id) =>
            ConvertToViewModel(_ctx.Conferences.FirstOrDefault(conf => conf.Id == id));

        public IEnumerable<ConferenceViewModel> GetAll() => ConvertToViewModel(_ctx.Conferences);

        public void SaveChanges() => _ctx.SaveChanges();

        public bool IsExist(Guid id) => _ctx.Conferences.Any(conf => conf.Id == id);


        public ConferenceViewModel GetByUniqueAddress(string address) =>
            ConvertToViewModel(_ctx.Conferences.FirstOrDefault(a => a.UniqueAddress == address));

        public IEnumerable<ConferenceViewModel> GetLatest(ushort count) =>
            ConvertToViewModel(_ctx.Conferences.OrderByDescending(a => a.DateCreated).Take(count));


        public void Create(NewConferenceViewModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var current = _ctx.Conferences.FirstOrDefault(a => a.IsActual);
            if (current != null) current.IsActual = false;
            else current = null;

            var dateNow = DateTime.Now;

            var newConference = new Conference(entity.Admins, entity.Topics)
            {
                Id = Guid.NewGuid(),
                UniqueAddress = entity.UniqueAddress,
                IsActual = true,
                Title = entity.Title,

                ShortDescription = entity.ShortDescription,
                Description = entity.Description,

                DateCreated = dateNow,
                DateLastModified = dateNow,
                DateStart = entity.DateStart,
                DateFinish = entity.DateFinish
            };

            _ctx.Conferences.Add(newConference);

            foreach (var admin in newConference.Admins)
            {
                admin.Conference = newConference;
                _ctx.ConfAdmins.Add(admin);
            }

            SaveChanges();
        }

        public void Update(ConferenceViewModel model)
        {
            // TODO ...
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            _ctx.Conferences.Remove(new Conference {Id = id});
            SaveChanges();
        }


        private ConferenceViewModel ConvertToViewModel(Conference conf)
        {
            if (conf == null) return null;

            return new ConferenceViewModel
            {
                IsActual = conf.IsActual,

                Id = conf.Id,
                Title = conf.Title,
                ShortDescription = conf.ShortDescription,
                Description = conf.Description,

                Participants = GetConfParticipants(conf),
                Admins = GetConfAdmins(conf),
                Articles = GetConfArticles(conf),
                Images = GetConfImages(conf),

                DateCreated = conf.DateCreated,
                DateLastModified = conf.DateLastModified,
                DateStart = conf.DateStart,
                DateFinish = conf.DateFinish
            };
        }

        private IEnumerable<ConferenceViewModel> ConvertToViewModel
            (IEnumerable<Conference> conf) => conf.Select(ConvertToViewModel);
    }
}