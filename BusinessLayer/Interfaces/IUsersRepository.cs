using DatabaseLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IUsersRepository : ICollectedData<string, User>
    {
        User GetByEmail(string email);
    }
}