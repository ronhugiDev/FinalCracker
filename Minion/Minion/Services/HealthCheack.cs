using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Minion.Services
{
    public class HealthCheack : IHealthCheack
    {
        private readonly ServiceSettings _serviceSettings;
        private readonly HttpClient _httpClient;

        public HealthCheack(HttpClient httpClient,IOptions<ServiceSettings> options)
        {
            _httpClient = httpClient;
            _serviceSettings = options.Value;
        }
        public async Task<bool> IsMasterAlive()
        {
            try
            {
                var mastrUrl = _serviceSettings.MasterSetting.MasterUrl + _serviceSettings.MasterSetting.MasterIsAlive;
                var response = await _httpClient.GetAsync(mastrUrl);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
