using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DatabaseLayer.Entities;
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

        public DbSet<ConferenceImage> ConfImages { get; set; }

        // It needs to add the relations for the DataModels and the primary/foreign keys using EF Fluent API
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roleId = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(new User
            {
                Id = roleId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
            }); // It is better to describe the admin user in the startup file, e.g. 
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(RoleNames.ADMIN, policy =>
            //    {
            //        policy.RequireUserName("admin@gmail.com");
            //    });
            //});

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

        // Why do we need this method?
        public static void ClearDatabase(DbContext context)
        {
            var objectContext = ((IObjectContextAdapter) context).ObjectContext;
            var entities = objectContext.MetadataWorkspace
                .GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace).BaseEntitySets;
            var method = objectContext.GetType().GetMethods().First(x => x.Name == "CreateObjectSet");
            var objectSets = entities.Select(x =>
                    method.MakeGenericMethod(Type.GetType(x.ElementType.FullName)))
                .Select(x => x.Invoke(objectContext, null));
            var tableNames = objectSets.Select(objectSet =>
                (objectSet.GetType().GetProperty("EntitySet")
                    .GetValue(objectSet, null) as EntitySet).Name).ToList();

            foreach (var tableName in tableNames)
            {
                context.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0}", tableName));
            }

            context.SaveChanges();
        }
    }
}