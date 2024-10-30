using Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public SubcriptionEnum Type { get; set; }
        public int MaxConversions { get; set; }
    }
}
