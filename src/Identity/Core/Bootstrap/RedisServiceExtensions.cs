using Common.Persistence.DistributedCache.Redis;
using Common.Persistence.Redis;

namespace Identity.Core.Bootstrap;

public static class RedisServiceExtensions
{
    public static IServiceCollection AddConfiguredRedisCache(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = configuration.GetSection(RedisConfig.Key).Get<RedisConfig>();

        // Distributed caching
        services.AddConfiguredRedisCache("identity", config);

        return services;
    }
}