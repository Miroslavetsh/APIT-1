using DatabaseLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IUsersRepository : ICollectedData<string, User, User>, IAddressedData<User>
    {
        User GetByEmail(string email);
    }
}