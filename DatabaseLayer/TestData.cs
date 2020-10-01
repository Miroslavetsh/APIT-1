/*
using System;
using System.Linq;
using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace DatabaseLayer
{
    public static class TestData
    {
        public static void ClearDatabase(AppDbContext context)
        {
            context.Articles.RemoveRange(context.Articles);
            context.Users.RemoveRange(context.Users);
            context.Topics.RemoveRange(context.Topics);
            context.SaveChanges();
        }


        public static void Apply(AppDbContext context)
        {
            ApplyUsers(context);

            ApplyTopics(context);

            context.SaveChanges();

            ApplyArticles(context);

            context.SaveChanges();
        }


        private static void ApplyUsers(AppDbContext context)
        {
            if (context.Users.Any()) return;

            context.Users.Add(new User()
            {
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "test1234"),
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                EmailConfirmed = true
            });
            context.Users.Add(new User()
            {
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "1234"),
                FirstName = "Іван",
                LastName = "Креативний",
                MiddleName = "Поповий",
                Email = "jojotop@jojofanclub.com",
                EmailConfirmed = true
            });
            context.Users.Add(new User()
            {
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "1234"),
                FirstName = "Ліліт",
                LastName = "Ковбасенко",
                MiddleName = "Батьківновна",
                Email = "ludka.from1956@email.net",
                EmailConfirmed = true
            });
            context.Users.Add(new User()
            {
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "1234"),
                FirstName = "Василько",
                LastName = "Василенко",
                MiddleName = "Васильович",
                Email = "fucker69@gmail.com",
                EmailConfirmed = true
            });
        }

        private static void ApplyTopics(AppDbContext context)
        {
            if (context.Topics.Any()) return;

            context.Topics.Add(new Topic
            {
                Id = Guid.NewGuid(),
                Name = "COVID-19"
            });
            context.Topics.Add(new Topic
            {
                Id = Guid.NewGuid(),
                Name = "Cute kittens"
            });
            context.Topics.Add(new Topic
            {
                Id = Guid.NewGuid(),
                Name = "Rock-n-Roll"
            });
        }

        private static DateTime GetDateTime(string s) =>
            DateTime.ParseExact(s, "yyyy-MM-dd HH:mm", null);

        private static void ApplyArticles(AppDbContext context)
        {
            if (context.Articles.Any()) return;

            var topics = new[]
            {
                context.Topics.First(t => t.Name == "COVID-19").Id,
                context.Topics.First(t => t.Name == "Cute kittens").Id,
                context.Topics.First(t => t.Name == "Rock-n-Roll").Id,
            };


            context.Articles.Add(new Article
            {
                Id = Guid.NewGuid(),
                TopicId = topics[0],
                Title = "COVID-19 Dashboard by the Center for Systems Science and Engineering (CSSE)",
                KeyWords = "pandemic official",
                DateCreated = GetDateTime("2020-08-11 21:44"),
                DateLastModified = GetDateTime("2020-08-21 21:44")
            });
            context.Articles.Add(new Article
            {
                Id = Guid.NewGuid(),
                TopicId = topics[0],
                Title = "Interactive tracker offers users map and graphical displays",
                KeyWords = "pandemic ukraine",
                DateCreated = GetDateTime("2020-08-16 10:40"),
                DateLastModified = GetDateTime("2020-08-16 10:40")
            });


            context.Articles.Add(new Article
            {
                Id = Guid.NewGuid(),
                TopicId = topics[1],
                Title = "29 Of The Most Beautiful Cats In The World ",
                KeyWords = "cats cat top",
                DateCreated = GetDateTime("2020-09-11 20:00"),
                DateLastModified = GetDateTime("2020-09-11 20:01")
            });
            context.Articles.Add(new Article
            {
                Id = Guid.NewGuid(),
                TopicId = topics[1],
                Title = "Meet Smoothie, World’s Most Photogenic Cat",
                KeyWords = "cat",
                DateCreated = GetDateTime("2020-09-01 00:05"),
                DateLastModified = GetDateTime("2020-09-01 00:05")
            });
            context.Articles.Add(new Article
            {
                Id = Guid.NewGuid(),
                TopicId = topics[1],
                Title = "Coby With The Most Beautiful Eyes Ever",
                KeyWords = "cat eyes",
                DateCreated = GetDateTime("2020-09-15 05:15"),
                DateLastModified = GetDateTime("2020-09-15 05:15")
            });
        }
    }
}
*/