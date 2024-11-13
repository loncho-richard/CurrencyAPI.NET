using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Common.Models
{
    public class CurrencyDTO
    {
        public CurrencyEnum Code { get; set; }
        public string Leyend { get; set; }
        public float Value { get; set; }
    }
}
