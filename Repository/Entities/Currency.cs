using Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public CurrencyEnum Code { get; set; }
        public float Value { get; set; }
    }
}
