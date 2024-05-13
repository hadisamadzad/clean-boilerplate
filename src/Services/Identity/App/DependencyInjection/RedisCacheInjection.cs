using Communal.Application.Configs;
using Communal.App.DependencyInjection;

namespace Identity.App.DependencyInjection;

public static class RedisCacheInjection
{
    public static IServiceCollection AddConfiguredRedisCache(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = configuration.GetSection(RedisCacheConfig.Key).Get<RedisCacheConfig>();

        // Distributed caching
        services.AddStackExchangeRedis("identity", config);

        return services;
    }
}