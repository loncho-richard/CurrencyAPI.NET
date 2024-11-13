using Common.Enums;
using Common.Models;
using Data.Entities;
using Data.Repository.Interfaces;

namespace Data.Repository.Implementations
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyAPIContext _context;
        public CurrencyRepository(CurrencyAPIContext currencyAPIContext) 
        {
            _context = currencyAPIContext;
        }

        public IEnumerable<Currency> GetAll()
        {
            return _context.Currencys.ToList();
        }

        public Currency GetOne(int currencyId)
        {
            return _context.Currencys.SingleOrDefault(c => c.Id == currencyId);
        }

        public Currency GetByCode(CurrencyEnum currencyCode)
        {
            try
            {
                return _context.Currencys.SingleOrDefault(c => c.Code == currencyCode);
            }
            catch (Exception) 
            {
                throw new Exception();
            }
        }

        public int CreateCurrency(Currency currency)
        {
            try
            {
                _context.Currencys.Add(currency);
                _context.SaveChanges();
                return _context.Currencys.Max(c => c.Id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public int UpdateCurrency(Currency currency)
        {
            try
            {
                var existingCurrency = _context.Currencys.SingleOrDefault(c => c.Id == currency.Id);

                existingCurrency.Code = currency.Code;
                existingCurrency.Value = currency.Value;
                _context.SaveChanges();

                return existingCurrency.Id;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error updating currency: {ex}");
            }
        }

        public void DeleteCurrency(int currencyId)
        {
            try
            {
                var currency = _context.Currencys.SingleOrDefault(c => c.Id == currencyId);

                _context.Currencys.Remove(currency);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting currency: {ex}");
            }
        }
    }
}
