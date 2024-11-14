using Common.Models;
using Data.Entities;
using Data.Repository.Interfaces;
using Services.Interfaces;


namespace Services.Implementation
{
    public class UserServices : IUserServices
    {
        public readonly IUserRepository _userRepository;
        public readonly ISubscriptionRepository _subscriptionRepository;
        public UserServices(IUserRepository userRepository, ISubscriptionRepository subscriptionRepository)
        {
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public User? AuthUser(CredentialsDTO credentialsDTO)
        {
            return _userRepository.AuthUser(credentialsDTO);
        }

        public int CreateUser(NewUserDTO newUserDTO)
        {
            if (_userRepository.ReadUsers().Any(u => u.Username == newUserDTO.Username))
            {
                throw new Exception("User already exists");
            }

            var subscription = _subscriptionRepository.GetAll()
                              .FirstOrDefault(s => s.Type == newUserDTO.Subscription);

            if (subscription == null)
            {
                throw new Exception("Subscription type not found.");
            }

            try
            {
                int newUserId = _userRepository.CreateUser(
                    new User
                    {
                        Username = newUserDTO.Username,
                        Password = newUserDTO.Password,
                        Conversions = newUserDTO.Conversions,
                        SubscriptionId = subscription.Id
                    });

                return newUserId;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public User GetOneById(int userId)
        {
            return _userRepository.GetOneById(userId);
        }

        public void UpdateUserConversions(int userId, int newConversionCount) 
        {
            User user = _userRepository.GetOneById(userId);
            if (user == null) 
            {
                throw new Exception("User not found");
            }

            user.Conversions = newConversionCount;
            _userRepository.UpdateUser(user);
        }
    }
}
