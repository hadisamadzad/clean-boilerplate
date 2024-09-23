using Common.Api.HealthChecks;

namespace Gateway.Core.DependencyInjection;

public static class HealthCheckInjection
{
    public static IServiceCollection AddConfiguredHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<GeneralHealthCheck>("general-check");

        return services;
    }
}
