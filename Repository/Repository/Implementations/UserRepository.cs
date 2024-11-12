using Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly CurrencyAPIContext _context;
        public UserRepository(CurrencyAPIContext currencyAPIContect)
        {
            _context = currencyAPIContect;
        }

    }
}
