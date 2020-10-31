﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Entities;

namespace BusinessLayer.Models
{
    public class ConferenceViewModel
    {
        public bool IsActual { get; set; }

        public Guid Id { get; set; }

        public string UniqueAddress { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public IEnumerable<User> Participants { get; set; }


        // The admin user must have access to the whole conferences data independently of the active instance of the conference.
        public IEnumerable<User> Admins { get; set; }

        public string[] AdminKeys { get; set; }

        public IEnumerable<ArticleViewModel> Articles { get; set; }
        public IEnumerable<string> Images { get; set; }

        public IEnumerable<Topic> Topics { get; set; }


        [DataType(DataType.Date)] public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)] public DateTime DateLastModified { get; set; }

        [DataType(DataType.DateTime)] public DateTime DateStart { get; set; }
        [DataType(DataType.DateTime)] public DateTime DateFinish { get; set; }
    }
}