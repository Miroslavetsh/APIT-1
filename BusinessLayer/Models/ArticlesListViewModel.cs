using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class ArticlesListViewModel
    {
        public IEnumerable<ArticleViewModel> Collection { get; set; }
        public string Filter { get; set; } = "all";
    }
}