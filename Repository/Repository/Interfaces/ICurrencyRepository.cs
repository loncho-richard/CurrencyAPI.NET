using Common.Enums;
using Common.Models;
using Data.Entities;


namespace Data.Repository.Interfaces
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetAll();
        Currency GetOne(int currencyId);
        Currency GetByCode(CurrencyEnum currencyCode);
        int CreateCurrency(Currency currency);
        int UpdateCurrency(Currency currency);
        void DeleteCurrency(int currencyId);
    }
}
