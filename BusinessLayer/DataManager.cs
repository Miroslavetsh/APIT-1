using BusinessLayer.Interfaces;

namespace BusinessLayer
{
    public class DataManager
    {
        public IUsersRepository Users { get; }
        public IArticlesRepository Articles { get; }
        public ITopicsRepository Topics { get; }


        public DataManager(IUsersRepository users, IArticlesRepository articles, ITopicsRepository topics)
        {
            Users = users;
            Articles = articles;
            Topics = topics;
        }
    }
}