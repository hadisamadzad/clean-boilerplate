using Communal.Application.Configurations;
using Identity.Application.Configurations;
using Identity.Application.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions.DependencyInjection
{
    public static class ConfigurationInjection
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            // Jwt helper static config
            JwtHelper.Config = configuration.GetSection(SecurityTokenConfig.Key).Get<SecurityTokenConfig>();

            // User helper static lockout config
            UserHelper.LockoutConfig = configuration.GetSection(LockoutConfig.Key).Get<LockoutConfig>();

            services.Configure<RedisCacheConfig>(configuration.GetSection(RedisCacheConfig.Key));
            services.Configure<RabbitMQConfig>(configuration.GetSection(RabbitMQConfig.Key));

            return services;
        }
    }
}