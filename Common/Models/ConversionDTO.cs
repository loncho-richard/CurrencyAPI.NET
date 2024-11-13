
using Common.Enums;

namespace Common.Models
{
    public class ConversionDTO
    {
        public CurrencyEnum FromCurrency { get; set; }
        public CurrencyEnum ToCurrency { get; set; }
        public float Amount { get; set; }
    }
}
