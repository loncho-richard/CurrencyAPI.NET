using Data.Entities;
using Data.Repository.Interfaces;


namespace Data.Repository.Implementations
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly CurrencyAPIContext _context;
        public SubscriptionRepository(CurrencyAPIContext currencyAPIContext) 
        {
            _context = currencyAPIContext;
        }

        public IEnumerable<Subscription> GetAll()
        {
            return _context.Subscriptions.ToList();
        }

        public Subscription GetOne(int subcriptionId)
        {
            return _context.Subscriptions.SingleOrDefault(s => s.Id == subcriptionId);
        }

        public int CreateSubscription(Subscription subscription)
        {
            try
            {
                _context.Subscriptions.Add(subscription);
                _context.SaveChanges();
                return _context.Subscriptions.Max(s => s.Id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public int UpdateSubscription(Subscription subscription) 
        {
            try
            {
                var existingSubscription = _context.Subscriptions.SingleOrDefault(s => s.Id == subscription.Id);

                existingSubscription.Type = subscription.Type;
                existingSubscription.MaxConversions = subscription.MaxConversions;
                _context.SaveChanges();

                return existingSubscription.Id;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error updating subscription: {ex}");
            }
        }

        public void DeleteSubscription(int subscriptionId)
        {
            try
            {
                var subscription = _context.Subscriptions.SingleOrDefault(s => s.Id == subscriptionId);

                _context.Subscriptions.Remove(subscription);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting subscription: {ex}");
            }
        }
    }
}
