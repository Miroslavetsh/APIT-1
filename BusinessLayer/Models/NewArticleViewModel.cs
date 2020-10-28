using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Models
{
    // Why don't we use the ArticleViewModel for represent the new instance of the Article?
    public class NewArticleViewModel
    {
        [Display(Name = "Унікальна адреса")]  // It is better to use the Localization and the Resource files.
        [Required]
        public string UniqueAddress { get; set; } // What does it mean?

        [Display(Name = "Файл із матеріалами")] // It is better to use the Localization and the Resource files.
        [Required]
        public IFormFile DocFile { get; set; }

        [Display(Name = "Тематика")] // It is better to use the Localization and the Resource files.
        [Required]
        public string TopicId { get; set; }

        [Display(Name = "Ключові слова")] // It is better to use the Localization and the Resource files.
        [Required]
        public string KeyWords { get; set; }

        [Display(Name = "Заголовок")] // It is better to use the Localization and the Resource files.
        [Required]
        public string Title { get; set; }
    }
}