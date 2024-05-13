using Communal.Application.Configs;
using Communal.App.DependencyInjection;

namespace Identity.App.Registrations;

public static class RedisRegistration
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