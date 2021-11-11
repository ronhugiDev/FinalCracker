using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minion.Repositories
{
    public sealed class PhoneBook:IPhoneBook
    {
        private static PhoneBook instance = null;
        private static readonly object padlock = new object();
        public static Dictionary<string, string> Passwords { get; set; }

        PhoneBook()
        {
        }

        public static PhoneBook Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null || Passwords == null)
                    {
                        instance = new PhoneBook();
                        Passwords = new Dictionary<string, string>();
                    }
                    return instance;
                }
            }
        }
    }
}
