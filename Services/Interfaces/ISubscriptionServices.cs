using Common.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISubscriptionServices
    {
        IEnumerable<Subscription> GetAll();
        Subscription GetOne(int subscriptionId);
        int CreateSubscription(SubscriptionDTO subscriptionDTO);
        int UpdateSubscription(int subscriptionId, SubscriptionDTO updateSubscription);
        void DeleteSubscription(int subscriptionId);
    }
}
