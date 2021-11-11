using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Master.Dtos;

namespace Master.Services
{
    public class FileProcess : IFileProcess
    {
        public IList<Passwords> ConvertJsonFile(IFormFile file)
        {
            string jsonStr = string.Empty;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                jsonStr = Encoding.UTF8.GetString(fileBytes);
            }
            return  JsonConvert.DeserializeObject<IList<Passwords>>(jsonStr);
        }
    }
}
