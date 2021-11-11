using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Minion
{
    public class MasterHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings _serviceSettings;
        private readonly HttpClient _httpClient;

        public MasterHealthCheck(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            _httpClient = httpClient;
            _serviceSettings = options.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var MasterUrl = _serviceSettings.MasterSetting.MasterUrl + _serviceSettings.MasterSetting.MasterIsAlive;
            var response = await _httpClient.GetAsync(MasterUrl);
            if (!response.IsSuccessStatusCode) {
                return HealthCheckResult.Unhealthy();
            }
            return HealthCheckResult.Healthy();
        }
    }
}
