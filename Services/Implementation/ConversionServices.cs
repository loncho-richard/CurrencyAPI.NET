

using Common.Models;
using Services.Interfaces;

namespace Services.Implementation
{
    public class ConversionServices : IConversionServices
    {
        public readonly ICurrencyServices _currencyServices;
        public ConversionServices(ICurrencyServices currencyServices) 
        {
            _currencyServices = currencyServices;
        }

        public float ConvertCurrency(ConversionDTO conversionDTO) 
        {


            var fromCurrency = _currencyServices.GetByCode(conversionDTO.FromCurrency);
            var toCurrency = _currencyServices.GetByCode(conversionDTO.ToCurrency);

            if (fromCurrency == null || toCurrency == null) 
            {
                throw new Exception("One or both currency codes are invalid");
            }

            float amountInDollars = conversionDTO.Amount * fromCurrency.Value;
            float result = amountInDollars / toCurrency.Value;

            return result;
        }
    }
}
