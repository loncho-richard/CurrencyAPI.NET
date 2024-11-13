using Common.Models;
using Data.Entities;
using Data.Repository.Interfaces;
using Services.Interfaces;

namespace Services.Implementation
{
    public class CurrencyServices : ICurrencyServices
    {
        public readonly ICurrencyRepository _currencyRepository;
        public CurrencyServices(ICurrencyRepository currencyRepository) 
        {
            _currencyRepository = currencyRepository;
        }

        public IEnumerable<Currency> GetAll() 
        {
            return _currencyRepository.GetAll();
        }

        public Currency GetOne(int id)
        {
            return _currencyRepository.GetOne(id);
        }

        public int CreateCurrency(CurrencyDTO newCurrencyDTO)
        {
            if (_currencyRepository.GetAll().Any(c => c.Code == newCurrencyDTO.Code))
            {
                throw new Exception("Currency already exists");
            }

            try
            {
                int newCurrencyId = _currencyRepository.CreateCurrency(
                    new Currency
                    {
                        Code = newCurrencyDTO.Code,
                        Value = newCurrencyDTO.Value
                    });
                return newCurrencyId;
            }
            catch (Exception) 
            {
                throw new Exception();
            }
        }

        public int UpdateCurrency(int currencyId, CurrencyDTO updateCurrency) 
        {
            var currency = _currencyRepository.GetOne(currencyId);
            if (currency == null)
            {
                throw new Exception("Currency not found");
            }

            currency.Code = updateCurrency.Code;
            currency.Value = updateCurrency.Value;

            return _currencyRepository.UpdateCurrency(currency);
        }

        public void DeleteCurrency(int currencyId) 
        {
            var currency = _currencyRepository.GetOne(currencyId);
            if (currency == null)
            {
                throw new Exception("Currency not found");
            }

            _currencyRepository.DeleteCurrency(currencyId);
        }
    }
}
