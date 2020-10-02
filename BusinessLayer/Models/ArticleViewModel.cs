using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Entities;
using DatabaseLayer.Enums;

namespace BusinessLayer.Models
{
    public class ArticleViewModel
    {
        public string Id { get; set; }

        public Topic Topic { get; set; }
        public User Creator { get; set; }

        public string Title { get; set; }
        public ArticleStatus Status { get; set; }

        public IEnumerable<string> KeyWords { get; set; }

        public string HTML { get; set; }

        [DataType(DataType.Time)] public DateTime DateCreated { get; set; }
        [DataType(DataType.Time)] public DateTime DateLastModified { get; set; }
    }
}