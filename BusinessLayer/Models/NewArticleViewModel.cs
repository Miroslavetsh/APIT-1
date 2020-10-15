using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class NewArticleViewModel
    {
        public string UniqueAddress { get; set; }

        public bool UseFromFile { get; set; }
        public byte[] ArticleFile { get; set; }

        [Display(Name = "Topic")] public string TopicId { get; set; }
        public bool CreateNewTopic { get; set; }
        [Display(Name = "Create new topic")] public string NewTopicName { get; set; }
        [Required] public string KeyWords { get; set; }

        [Required] public string Title { get; set; }
        [Required] public string TextHTML { get; set; }
    }
}