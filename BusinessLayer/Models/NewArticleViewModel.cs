using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Models
{
    public class NewArticleViewModel
    {
        [Display(Name = "Унікальна адреса")]
        [Required]
        public string UniqueAddress { get; set; }

        [Display(Name = "Файл із матеріалами")]
        [Required]
        public IFormFile DocFile { get; set; }

        [Display(Name = "Тематика")]
        [Required]
        public string TopicId { get; set; }

        [Display(Name = "Ключові слова")]
        [Required]
        public string KeyWords { get; set; }

        [Display(Name = "Заголовок")]
        [Required]
        public string Title { get; set; }
    }
}