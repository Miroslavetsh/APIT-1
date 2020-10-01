using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class NewArticleViewModel
    {
        public bool UseFromFile { get; set; }
        public byte[] ArticleFile { get; set; }

        public bool CreateNewTopic { get; set; }
        [Required] public string Topic { get; set; }
        [Required] public string KeyWords { get; set; }

        [Required] public string Title { get; set; }
        [Required] public string TextHTML { get; set; }
    }
}