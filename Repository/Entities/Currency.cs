using Common.Enums;
using Microsoft.EntityFrameworkCore;


namespace Data.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public CurrencyEnum Code { get; set; }
        public string Leyend { get; set; }
        public float Value { get; set; }
    }
}
