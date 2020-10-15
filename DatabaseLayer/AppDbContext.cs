using System;
using DatabaseLayer.Entities;
using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public override DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<ConferenceParticipant> ConfParticipants { get; set; }
        public DbSet<ConferenceAdmin> ConfAdmins { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roleId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(new User
            {
                Id = roleId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
            });

            /*
            var userId = Guid.NewGuid().ToString();
            builder.Entity<User>().HasData(new User()
            {
                Id = userId,
                FirstName = "Big",
                LastName = "Boss",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "test@test.com",
                NormalizedEmail = "TEST@TEST.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>()
                    .HashPassword(null, "admin"),

                ScienceDegree = ScienceDegree.First,
                AcademicTitle = AcademicTitle.BestOfTheBest,
                ParticipationForm = ParticipationForm.Admin,

                ProfileAddress = "admin-the-best"
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId
            });
            */
        }
    }
}