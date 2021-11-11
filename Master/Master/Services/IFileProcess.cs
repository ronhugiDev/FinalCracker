using Master.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Services
{
    public interface IFileProcess
    {
        IList<Passwords> ConvertJsonFile(IFormFile file);
    }
}
