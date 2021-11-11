using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Entities
{
    public record MinionData
    {
        public int StartRange { get; set; }
        public int EndRange { get; set; }
        public string Url { get; set; }
    }
}
