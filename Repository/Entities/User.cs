using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Conversions { get; set; }
        public int SubcriptionId  { get; set; }
        public Subscription Subscription { get; set; }

    }
}
