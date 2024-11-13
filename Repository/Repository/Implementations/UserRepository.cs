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
    }
}
