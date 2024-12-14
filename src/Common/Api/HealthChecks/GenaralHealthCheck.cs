using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Common.Api.HealthChecks;

public class GeneralHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new ())
    {
        return HealthCheckResult.Healthy("Everything is Ok!");
    }
}
