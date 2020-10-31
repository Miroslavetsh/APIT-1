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

            conference.Participants.Add(new ConferenceParticipant
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Conference = conference
            });
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
                conference.Participants.Remove(new ConferenceParticipant {Id = participant.Id});

            conference.Admins.Add(new ConferenceAdmin
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Conference = conference
            });
        }

        public void AddArticle(Conference conference, Article article)
        {
            if (conference.Articles.Contains(article))
            {
                Console.WriteLine($"Conference {conference.Id} already contains article {article.Id}");
                return;
            }

            conference.Articles.Add(article);
        }

        public void AddImage(Conference conference, IFormFile image)
        {
            var extension = "." + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(DataUtil.IMAGES_DIR, Guid.NewGuid() + extension);
            DataUtil.SaveFile(image, filePath);

            conference.Images.Add(new ConferenceImage
            {
                Id = Guid.NewGuid(),
                ImagePath = filePath,
                Conference = conference,
            });
        }
    }
}