
using Common.Models;

namespace Services.Interfaces
{
    public interface IConversionServices
    {
        float ConvertCurrency(ConversionDTO conversionDTO);
    }
}
