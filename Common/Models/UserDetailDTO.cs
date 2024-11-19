using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public int Conversions { get; set; }
        public int SubscriptionId { get; set; }
        
    }
}

