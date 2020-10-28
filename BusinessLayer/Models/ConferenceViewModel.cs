using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Entities;

namespace BusinessLayer.Models
{
    public class ConferenceViewModel
    {
        public Guid Id { get; set; }

        public string UniqueAddress { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public IEnumerable<User> Participants { get; set; }


        // The admin user must have access to the whole conferences data independently of the active instance of the conference.
        public IEnumerable<User> Admins { get; set; } // It is better to use the Role property for each user and to restrict access to controllers/actions using specific attribute (e.g. [Authorize(Roles = RoleNames.ADMIN)])


        public IEnumerable<ArticleViewModel> Articles { get; set; }
        public IEnumerable<string> Images { get; set; }

        [DataType(DataType.Date)] public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)] public DateTime DateLastModified { get; set; }
        [DataType(DataType.DateTime)] public DateTime DateStart { get; set; }
        [DataType(DataType.DateTime)] public DateTime DateFinish { get; set; }
    }
}