using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minion.Dtos
{
    public record PhoneNumberDto
    {
        public string FullNumber { get; set; }
        public string HashedNumber { get; set; }
    }
}
