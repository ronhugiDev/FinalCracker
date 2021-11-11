using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Services
{
    public interface IMinionServices
    {
        Task<string> Post_GetNumberFromHash(string hashNumber);
        Task<List<string>> MinionsAliveCheck();
    }
}
