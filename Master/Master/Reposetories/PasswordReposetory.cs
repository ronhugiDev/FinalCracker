using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Reposetories
{
    //can be an outer db like mongoDB 
    public sealed class PasswordReposetory : IPasswordReposetory 
    {
        private static PasswordReposetory instance = null;
        private static readonly object padlock = new object();
        public static Dictionary<string, string> Passwords { get; set; }

        PasswordReposetory()
        {
        }

        public static PasswordReposetory Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null || Passwords==null)
                    {
                        instance = new PasswordReposetory();
                        Passwords = new Dictionary<string, string>();
                    }
                    return instance;
                }
            }
        }
    }
}
