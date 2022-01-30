using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CaseStudies.Core
{
    public class ICMPHealthCheck : IHealthCheck
    {
        private string host = "www-does-not-exist.com";
        private int timeout = 300;
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(host);

                    switch (reply.Status)
                    {
                        case IPStatus.Success:
                            return (reply.RoundtripTime > timeout) ? HealthCheckResult.Degraded() : HealthCheckResult.Healthy();

                        default:
                            return HealthCheckResult.Unhealthy();
                    }
                }
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}
