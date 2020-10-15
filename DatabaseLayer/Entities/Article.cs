using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Enums;

namespace DatabaseLayer.Entities
{
    public class Article
    {
        //TODO: Convert Id from Guid to String format
        [Required] public Guid Id { get; set; }
        public string UniqueAddress { get; set; }
        [Required] public Guid TopicId { get; set; }
        [Required] public string CreatorId { get; set; }

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

        /// <summary>
        /// MS Word file with article content
        /// (use relative path)
        /// </summary>
        public string DataFile { get; set; }


        public Conference Conference { get; set; }

        [DataType(DataType.DateTime)] public DateTime DateCreated { get; set; }
        [DataType(DataType.DateTime)] public DateTime DateLastModified { get; set; }
    }
}