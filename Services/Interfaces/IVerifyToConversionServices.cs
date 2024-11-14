

using Common.Models;

namespace Services.Interfaces
{
    public interface IVerifyToConversionServices
    {
        float VerifyToConversion(int userId, ConversionDTO conversionDTO);
    }
}
