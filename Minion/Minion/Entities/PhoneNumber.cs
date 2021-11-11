using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minion.Entities
{
    public record PhoneNumber
    {
        public string FullNumber { get; set; }
        public string HashedNumber { get; set; }
    }
}
