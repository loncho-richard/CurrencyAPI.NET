using Common.Enums;
using Common.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICurrencyServices
    {
        IEnumerable<Currency> GetAll();
        Currency GetOne(int currencyId);
        Currency GetByCode(CurrencyEnum currencyCode);
        int CreateCurrency(CurrencyDTO newCurrencyDTO);
        int UpdateCurrency(int currencyId, CurrencyDTO updateCurrency);
        void DeleteCurrency(int currencyId);
    }
}
