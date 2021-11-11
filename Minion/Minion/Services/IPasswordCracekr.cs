using Minion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Minion.Services
{
    public interface IPasswordCracekr
    {
        PhoneNumber CrackPassword(string hashPassword, int startRange, int endRange);
        /// <summary>
        /// crack the hash
        /// </summary>
        /// <param name="hashPassword">the given pass</param>
        /// <param name="startRange">from where ot start the bourte froce</param>
        /// <param name="endRange">till where to stope</param>
        /// <returns></returns>
        string CrackWithRanges(string hashPassword,int startRange,int endRange);
        string MD5Hash(string number);
    }

}
