using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Master.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;

namespace Master.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MasterCrackerController : ControllerBase
    {
        private readonly IMinionServices _minionServices;
        private readonly IFileProcess _fileProcess;

        private readonly ILogger<MasterCrackerController> _logger;
        public MasterCrackerController(IMinionServices minionServices,
            IFileProcess fileProcess,
            ILogger<MasterCrackerController> logger)
        {
            _minionServices = minionServices;
            _fileProcess = fileProcess;
            _logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<string>> LoadHashedPasswordFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                var obs = _fileProcess.ConvertJsonFile(file);
                foreach (var item in obs)
                {
                    try
                    {
                        await _minionServices.Post_GetNumberFromHash(item.HashPassword);
                    }
                    catch (HttpRequestException)
                    {
                        return NotFound("no minions has found");
                    }
                }
            }
            return Ok();
        }
        [HttpGet]
        public ActionResult IsAlive()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> CheckMinionsAlive()
        {
            try
            {
                var deadMinions = await _minionServices.MinionsAliveCheck();
                if (deadMinions.Count == 0)
                {
                    return Ok();
                }

                else
                {
                    return NotFound(deadMinions);
                }
            }
            catch
            {
                return NotFound("all minions are dead");
            }
        }
    }
}
