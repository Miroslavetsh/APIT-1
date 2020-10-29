using System;
using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Enums;

namespace DatabaseLayer.Entities
{
    public class Article
    {
        public Guid Id { get; set; }
        [Required] public string UniqueAddress { get; set; }

        [Required] public Guid TopicId { get; set; }
        [Required] public string CreatorId { get; set; }

        [Required] public string Title { get; set; }


        public ArticleStatus Status { get; set; }
        [Required] public string KeyWords { get; set; }


        [Required] public string HtmlFilePath { get; set; }
        [Required] public string DocxFilePath { get; set; }

        public Conference Conference { get; set; }


        [DataType(DataType.DateTime)] public DateTime DateCreated { get; set; }
        [DataType(DataType.DateTime)] public DateTime DateLastModified { get; set; }
    }
}