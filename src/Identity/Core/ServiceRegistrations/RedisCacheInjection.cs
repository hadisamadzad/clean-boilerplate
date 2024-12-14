using Common.Application.Configs;
using Common.App.DependencyInjection;

namespace Identity.Core.Registrations;

public static class RedisRegistration
{
    public static IServiceCollection AddConfiguredRedisCache(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = configuration.GetSection(RedisCacheConfig.Key).Get<RedisCacheConfig>();
        //services.Configure<RedisCacheConfig>(configuration.GetSection(RedisCacheConfig.Key));

        // Distributed caching
        services.AddStackExchangeRedis("identity", config);

        return services;
    }
}