using Gateway.DelegatingHandlers;
using Ocelot.DependencyInjection;

namespace Gateway.DependencyInjection;

public static class OcelotInjection
{
    public static IServiceCollection AddConfiguredOcelot(this IServiceCollection services)
    {
        services
            .AddOcelot()
            .AddDelegatingHandler<GlobalDelegatingHandler>(global: true);

        return services;
    }
}