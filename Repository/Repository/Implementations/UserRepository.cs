using Data.Entities;
using Data.Repository.Interfaces;
using Common.Models;


namespace Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly CurrencyAPIContext _context;
        public UserRepository(CurrencyAPIContext currencyAPIContext)
        {
            _context = currencyAPIContext;
        }

        public IEnumerable<User> ReadUsers()
        {
            return _context.Users.ToList();
        }

        public User? AuthUser(CredentialsDTO credentialsDTO)
        {
            return _context.Users.FirstOrDefault(u => u.Username == credentialsDTO.Username && u.Password == credentialsDTO.Password);
        }

        public int CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return _context.Users.Max(u => u.Id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public UserDetailDTO GetOneById(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            var userDetailDTO = new UserDetailDTO
            {
                Id = user.Id,
                Username = user.Username,
                Conversions = user.Conversions,
                SubscriptionId = user.SubscriptionId
            };

            return userDetailDTO;
        }

        public void UpdateUser(UserDetailDTO user)
        {
            User existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);

            existingUser.Conversions = user.Conversions;
            _context.SaveChanges();
        }

        public void AssignSubscription(int userId, Subscription subscription)
        {
            _context.Users.SingleOrDefault(u => u.Id == userId).SubscriptionId = subscription.Id;
            _context.SaveChanges();
        }
    }
}
