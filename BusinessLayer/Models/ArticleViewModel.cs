using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Entities;
using DatabaseLayer.Enums;

namespace BusinessLayer.Models
{
    public class ArticleViewModel
    {
        public string UniqueAddress { get; set; }
        public string DocFileAddress { get; set; }

        public string HTMLContent { get; set; }

        public Topic Topic { get; set; }
        public User Creator { get; set; } // If article has many authors?


        public string Title { get; set; }
        public ArticleStatus Status { get; set; }
        public IEnumerable<string> KeyWords { get; set; }


        [DataType(DataType.DateTime)] public DateTime DateCreated { get; set; }
        [DataType(DataType.DateTime)] public DateTime DateLastModified { get; set; }
    }
}