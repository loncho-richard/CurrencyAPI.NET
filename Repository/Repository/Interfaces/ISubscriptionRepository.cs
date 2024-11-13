using Data.Entities;


namespace Data.Repository.Interfaces
{
    public interface ISubscriptionRepository
    {
        IEnumerable<Subscription> GetAll();
        Subscription GetOne(int subscriptionId);
        int CreateSubscription(Subscription subscription);
        int UpdateSubscription(Subscription subscription);
        void DeleteSubscription(int subscriptionId);
    }
}
