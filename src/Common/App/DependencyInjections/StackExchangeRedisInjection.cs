using Common.Application.Configs;
using Common.Persistence.DistributedCache;
using Common.Persistence.DistributedCache.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Common.App.DependencyInjection;

public static class StackExchangeRedisInjection
{
    public static IServiceCollection AddStackExchangeRedis(this IServiceCollection services,
        string instancePrefix, RedisCacheConfig config)
    {
        var options = new ConfigurationOptions();
        foreach (var connection in config.Connections)
            options.EndPoints.Add(connection);

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options));

        services.AddScoped<ICache>(x =>
            new StackExchangeRedisCache(x.GetRequiredService<IConnectionMultiplexer>(),
                instancePrefix: instancePrefix));

        return services;
    }
}
