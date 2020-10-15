using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Models;
using DatabaseLayer.Entities;

namespace BusinessLayer.Repositories
{
    public partial class ConferenceRepository
    {
        private IEnumerable<User> GetConfParticipants(Conference conference) =>
            _ctx.ConfParticipants.Where(a => a.Conference == conference).Select(a => _users.GetById(a.Id));

        private IEnumerable<User> GetConfAdmins(Conference conference) =>
            _ctx.ConfAdmins.Where(a => a.Conference == conference).Select(a => _users.GetById(a.Id));

        private IEnumerable<ArticleViewModel> GetConfArticles(Conference conference) =>
            _ctx.Articles.Where(a => a.Conference == conference).Select(a => _articles.GetById(a.Id));
    }
}