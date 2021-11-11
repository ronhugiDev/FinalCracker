using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minion.Dtos;
using Minion.Services;
using System;
using System.Threading.Tasks;

namespace Minion.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PasswordMinionController : ControllerBase
    {
        private readonly ILogger<PasswordMinionController> _logger;
        private readonly IPasswordCracekr _passwordCracekr;
        private readonly IHealthCheack _healthCheack;
        public PasswordMinionController(IPasswordCracekr passwordCracekr, IHealthCheack healthCheack,ILogger<PasswordMinionController> logger)
        {
            _healthCheack = healthCheack;
            _passwordCracekr = passwordCracekr;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult IsAlive()
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult<PhoneNumberDto> GetPhoneNumbers(
            string hashPassword, int startRange ,int endRange)
        {
            try
            {
                var number = _passwordCracekr.CrackPassword(hashPassword,startRange,endRange);
                if (number is null || number.FullNumber == string.Empty)
                {
                    return NotFound();
                }
                return Ok(number.AsDto());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<ActionResult> CheckMasterHealth()
        {
            return await _healthCheack.IsMasterAlive() ? Ok() : NotFound("no master alive");
        }
    }
}
