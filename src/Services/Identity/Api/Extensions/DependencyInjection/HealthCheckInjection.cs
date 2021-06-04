using Communal.Api.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions.DependencyInjection
{
    public static class HealthCheckInjection
    {
        public static IServiceCollection AddConfiguredHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<GeneralHealthCheck>("general-check");

            return services;
        }
    }
}