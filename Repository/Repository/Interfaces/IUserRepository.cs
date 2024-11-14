using Common.Models;
using Data.Entities;


namespace Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> ReadUsers();
        User? AuthUser(CredentialsDTO credentialsDTO);
        int CreateUser(User user);
        User GetOneById(int userId);
        void UpdateUser(User user);
        void AssignSubscription(int userId, Subscription subscription);
    }
}
