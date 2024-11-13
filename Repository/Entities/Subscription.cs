using Common.Enums;


namespace Data.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public SubscriptionEnum Type { get; set; }
        public int MaxConversions { get; set; }
    }
}
