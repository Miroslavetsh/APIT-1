using System;
using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Enums;

namespace DatabaseLayer.Entities
{
    public class Article
    {
        [Required] public Guid Id { get; set; }
        [Required] public string UniqueAddress { get; set; }
        public Guid TopicId { get; set; }
        public string CreatorId { get; set; }

        public string Title { get; set; }
        [Required] public ArticleStatus Status { get; set; }

        /// <summary>
        /// Split keywords by space ' ' or comma ','
        /// </summary>
        /// <example>
        /// first second_key third other123
        /// </example>
        [Required]
        public string KeyWords { get; set; }

        [Required] public string HtmlFilePath { get; set; }
        [Required] public string DocxFilePath { get; set; }

        public Conference Conference { get; set; }

        [DataType(DataType.DateTime)] public DateTime DateCreated { get; set; }
        [DataType(DataType.DateTime)] public DateTime DateLastModified { get; set; }
    }
}