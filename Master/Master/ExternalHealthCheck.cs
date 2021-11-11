using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Master
{
    public class ExternalHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings _serviceSettings;
        public ExternalHealthCheck(IOptions<ServiceSettings> options)
        {
            _serviceSettings = options.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            for (int i = 0; i < _serviceSettings.MinionsSetting.Length; i++)
            {
                var reply = await ping.SendPingAsync(_serviceSettings.MinionsSetting[i].IsAliveFunction);
                if (reply.Status != IPStatus.Success)
                {
                    _serviceSettings.MinionsSetting[i].IsAlive = false;
                    return HealthCheckResult.Unhealthy();
                }          
            }
            return HealthCheckResult.Healthy();

        }
    }
}
