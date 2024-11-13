using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class SubscriptionDTO
    {
        public SubscriptionEnum Type { get; set; }
        public int MaxConversions { get; set; }
    }
}
