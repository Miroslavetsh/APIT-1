using BusinessLayer.Interfaces;

namespace BusinessLayer
{
    public class DataManager
    {
        public IUsersRepository Users { get; }
        public IArticlesRepository Articles { get; }
        public ITopicsRepository Topics { get; }
        public IConferencesRepository Conferences { get; }


        public DataManager(IUsersRepository users, IArticlesRepository articles,
            ITopicsRepository topics, IConferencesRepository conferences)
        {
            Users = users;
            Articles = articles;
            Topics = topics;
            Conferences = conferences;
        }
    }
}