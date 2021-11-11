using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minion.Entities
{
    public record PhoneNumberModel
    {
        public string Prefix { get; set; }
        public string PostFix { get; set; }
        public char Seperator => '-';
    }
}
