using Common.Models;
using Data.Entities;
using Data.Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class SubscriptionServices : ISubscriptionServices
    {
        public readonly ISubscriptionRepository _subscriptionRepository;
        public readonly IUserRepository _userRepository;
        public SubscriptionServices(ISubscriptionRepository subscriptionRepository, IUserRepository userRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
        }
        
        public IEnumerable<Subscription> GetAll()
        {
            return _subscriptionRepository.GetAll();
        }

        public Subscription GetOne(int subscriptionId)
        {
            return _subscriptionRepository.GetOne(subscriptionId);
        }

        public int CreateSubscription(SubscriptionDTO subscriptionDTO)
        {
            if (_subscriptionRepository.GetAll().Any(s => s.Type == subscriptionDTO.Type))
            {
                throw new Exception("Subscription plan already exists");
            }

            try
            {
                int newSubscriptionId = _subscriptionRepository.CreateSubscription(
                    new Subscription
                    {
                        Type = subscriptionDTO.Type,
                        MaxConversions = subscriptionDTO.MaxConversions,
                    });
                return newSubscriptionId;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public int UpdateSubscription(int subscriptionId, SubscriptionDTO updateSubscription)
        {
            var subscription = _subscriptionRepository.GetOne(subscriptionId) ?? throw new Exception("Subscription not found");
            subscription.Type = updateSubscription.Type;
            subscription.MaxConversions = updateSubscription.MaxConversions;

            return _subscriptionRepository.UpdateSubscription(subscription);
        }

        public void DeleteSubscription(int subscriptionId) 
        {
            Subscription subscription = _subscriptionRepository.GetOne(subscriptionId) ?? throw new Exception("Subscription not found");
            _subscriptionRepository.DeleteSubscription(subscriptionId);
        }

        public void AssignSubscription(int userId, int subscriptionId)
        {
            Subscription subscription = _subscriptionRepository.GetOne(subscriptionId) ?? throw new Exception("Subscription not found");
            _userRepository.AssignSubscription(userId, subscription);
        }
    }

}
