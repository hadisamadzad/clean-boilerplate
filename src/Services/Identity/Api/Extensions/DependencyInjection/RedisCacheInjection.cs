using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Identity.Persistence;
using Communal.Application.Configurations;
using Communal.Api.DependencyInjections;

namespace Identity.Extensions.DependencyInjection
{
    public static class RedisCacheInjection
    {
        public static IServiceCollection AddConfiguredRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection(RedisCacheConfig.Key).Get<RedisCacheConfig>();

            // Distributed caching
            services.AddStackExchangeRedis("identity", config);

            return services;
        }
    }
}