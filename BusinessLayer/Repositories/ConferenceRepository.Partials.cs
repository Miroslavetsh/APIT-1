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
        private IEnumerable<User> GetConfParticipants(Conference conference) =>
            _ctx.ConfParticipants.Where(a => a.Conference == conference).Select(a => _users.GetById(a.Id));

        private IEnumerable<User> GetConfAdmins(Conference conference) =>
            _ctx.ConfAdmins.Where(a => a.Conference == conference).Select(a => _users.GetById(a.Id));

        private IEnumerable<ArticleViewModel> GetConfArticles(Conference conference) =>
            _ctx.Articles.Where(a => a.Conference == conference).Select(a => _articles.GetById(a.Id));

        private IEnumerable<string> GetConfImages(Conference conference) =>
            _ctx.ConfImages.Where(a => a.Conference == conference).Select(a => a.ImagePath);
        
        
        public void AddParticipant(User user)
        {
            var current = GetCurrentAsDbModel();

            if (current.Participants.Any(a => a.Id == user.Id))
            {
                Console.WriteLine($"Conference {current.Id} already contains participant {user.Id}");
                return;
            }

            current.Participants.Add(new ConferenceParticipant
            {
                Id = user.Id,
                Conference = current
            });
        }

        public void AddAdmin(User user)
        {
            var current = GetCurrentAsDbModel();

            if (current.Admins.Any(a => a.Id == user.Id))
            {
                Console.WriteLine($"Conference {current.Id} already contains admin {user.Id}");
                return;
            }

            current.Admins.Add(new ConferenceAdmin
            {
                Id = user.Id,
                Conference = current
            });
        }

        public void AddArticle(Article article)
        {
            var current = GetCurrentAsDbModel();

            if (current.Articles.Contains(article))
            {
                Console.WriteLine($"Conference {current.Id} already contains article {article.Id}");
                return;
            }

            current.Articles.Add(article);
        }

        public void AddImage(IFormFile image)
        {
            var current = GetCurrentAsDbModel();

            var extension = "." + DataUtil.GetExtension(image.FileName);
            var filePath = Path.Combine(DataUtil.IMAGES_DIR, Guid.NewGuid() + extension);
            DataUtil.SaveFile(image, filePath);

            current.Images.Add(new ConferenceImage
            {
                Id = Guid.NewGuid(),
                ImagePath = filePath,
                Conference = current,
            });
        }
    }
}