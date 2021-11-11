using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Dtos
{
    public class Hashes
    {
        public IList<Passwords> Passwords { get; set; }
    }
    [Serializable]
    public class Passwords
    {
        public string HashPassword { get; set; }
    }
}
