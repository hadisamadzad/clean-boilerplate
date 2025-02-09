using Common.App.DependencyInjection;
using Common.Application.Configs;

namespace Identity.Core.Bootstrap;

public static class RedisServiceExtensions
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