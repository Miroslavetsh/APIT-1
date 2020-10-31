using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLayer.DataServices;
using BusinessLayer.Models;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Repositories
{
    public partial class ConferenceRepository
    {
        public IEnumerable<User> GetConfParticipants(Conference conference) =>
            _ctx.ConfParticipants.Where(a => a.Conference == conference).Select(a => _users.GetById(a.UserId));

        public IEnumerable<User> GetConfAdmins(Conference conference) =>
            _ctx.ConfAdmins.Where(a => a.Conference == conference).Select(a => _users.GetById(a.UserId));

        public IEnumerable<ArticleViewModel> GetConfArticles(Conference conference) =>
            _ctx.Articles.Where(a => a.Conference == conference).Select(a => _articles.GetById(a.Id));

        public IEnumerable<string> GetConfImages(Conference conference) =>
            _ctx.ConfImages.Where(a => a.Conference == conference).Select(a => a.ImagePath);


        public void AddParticipant(Conference conference, User user)
        {
            if (conference.Participants.Any(a => a.UserId == user.Id))
            {
                Console.WriteLine($"Conference {conference.Id} already contains participant {user.Id}");
                return;
            }

            var participant = new ConferenceParticipant
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Conference = conference
            };

            conference.Participants.Add(participant);
            _ctx.ConfParticipants.Add(participant);
        }

        public void AddAdmin(Conference conference, User user)
        {
            if (conference.Admins.Any(a => a.UserId == user.Id))
            {
                Console.WriteLine($"Conference {conference.Id} already contains admin {user.Id}");
                return;
            }

            var participant = conference.Participants.FirstOrDefault(a => a.UserId == user.Id);
            if (participant != null)
            {
                conference.Participants.Remove(new ConferenceParticipant {Id = participant.Id});
                _ctx.ConfParticipants.Remove(participant);
            }

            var organizer = new ConferenceAdmin
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Conference = conference
            };

            conference.Admins.Add(organizer);
            _ctx.ConfAdmins.Add(organizer);
        }

        public void AddArticle(Conference conference, Article article)
        {
            if (conference.Articles.Contains(article))
            {
                Console.WriteLine($"Conference {conference.Id} already contains article {article.Id}");
                return;
            }

            conference.Articles.Add(article);
            _ctx.Articles.Add(article);
        }

        public void AddImage(Conference conference, IFormFile imageFile)
        {
            var extension = "." + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(DataUtil.IMAGES_DIR, Guid.NewGuid() + extension);
            DataUtil.SaveFile(imageFile, filePath);

            var image = new ConferenceImage
            {
                Id = Guid.NewGuid(),
                ImagePath = filePath,
                Conference = conference,
            };

            conference.Images.Add(image);
            _ctx.ConfImages.Add(image);
        }


        public void RemoveParticipant(Conference conference, User user)
        {
            var participant = _ctx.ConfParticipants.FirstOrDefault(a => a.UserId == user.Id);
            if (participant == null) return;

            conference.Participants.Remove(participant);
            _ctx.ConfParticipants.Remove(participant);
        }

        public void RemoveAdmin(Conference conference, User user)
        {
            var organizer = _ctx.ConfAdmins.FirstOrDefault(a => a.UserId == user.Id);
            if (organizer == null) throw new KeyNotFoundException(nameof(user.Id));

            conference.Admins.Add(organizer);
            _ctx.ConfAdmins.Add(organizer);
        }

        public void RemoveArticle(Conference conference, Article article)
        {
            conference.Articles.Add(article);
            _ctx.Articles.Add(article);
        }

        public void RemoveImage(Conference conference, string path)
        {
            var image = conference.Images.FirstOrDefault(a => a.ImagePath == path);
            if (image == null) throw new KeyNotFoundException(nameof(path));

            conference.Images.Add(image);
            _ctx.ConfImages.Add(image);
        }
    }
}