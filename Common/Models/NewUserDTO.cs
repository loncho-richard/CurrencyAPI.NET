using Common.Enums;


namespace Common.Models
{
    public class NewUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Conversions { get; set; } = 0;
        public SubscriptionEnum Subscription { get; set; } = SubscriptionEnum.Free;
    }
}
