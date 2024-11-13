

namespace Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public int Conversions { get; set; }
        public int SubscriptionId  { get; set; }
        public Subscription Subscription { get; set; }

    }
}
