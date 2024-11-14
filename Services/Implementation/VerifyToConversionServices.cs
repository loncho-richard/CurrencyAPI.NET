

using Common.Models;
using Data.Entities;
using Services.Interfaces;

namespace Services.Implementation
{
    public class VerifyToConversionServices : IVerifyToConversionServices
    {
        private readonly IUserServices _userServices;
        private readonly ISubscriptionServices _subscriptionServices;
        private readonly IConversionServices _conversionServices;

        public VerifyToConversionServices(
            IUserServices userServices,
            ISubscriptionServices subscriptionServices,
            IConversionServices conversionServices)
        {
            _userServices = userServices;
            _subscriptionServices = subscriptionServices;
            _conversionServices = conversionServices;
        }

        public float VerifyToConversion(int userId, ConversionDTO conversionDTO)
        {
            var user = _userServices.GetOneById(userId) ?? throw new Exception("User not found");

            Subscription subscription = _subscriptionServices.GetOne(user.SubscriptionId) ?? throw new Exception("Subscription not found");

            int maxConversions = subscription.MaxConversions;
            if (maxConversions > 0 && user.Conversions >= maxConversions)
                throw new Exception("You hace reached the maxim number of conversions allowed by your subscription plan.");

            user.Conversions += 1;
            _userServices.UpdateUserConversions(userId, user.Conversions);

            return _conversionServices.ConvertCurrency(conversionDTO);
        }
    }
}
