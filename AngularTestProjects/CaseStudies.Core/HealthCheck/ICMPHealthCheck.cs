using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CaseStudies.Core.HealthCheck
{
    public class ICMPHealthCheck : IHealthCheck
    {
        private string host;
        private int timeout;
        public ICMPHealthCheck(string host, int timeout)
        {
            this.host = host;
            this.timeout = timeout;
        }
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
                            var msg = $"ICMP to {host} took {reply.RoundtripTime} ms.";
                            return (reply.RoundtripTime > timeout) ? HealthCheckResult.Degraded(msg) : HealthCheckResult.Healthy(msg);

                        default:
                            return HealthCheckResult.Unhealthy();
                    }
                }
            }
            catch (Exception e)
            {
                var msg = $"ICMP to {host} failed: {e.Message}";
                return HealthCheckResult.Unhealthy(msg);
            }
        }
    }
}
