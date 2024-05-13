using Communal.Application.Configs;
using Identity.Application.Helpers;
using Identity.Application.Types.Configs;

namespace Identity.App.DependencyInjection;

public static class ConfigurationInjection
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        // Jwt helper static config
        JwtHelper.Config = configuration.GetSection(SecurityTokenConfig.Key).Get<SecurityTokenConfig>();

        // User helper static lockout config
        UserHelper.LockoutConfig = configuration.GetSection(LockoutConfig.Key).Get<LockoutConfig>();

        services.Configure<RedisCacheConfig>(configuration.GetSection(RedisCacheConfig.Key));
        services.Configure<ActivationConfig>(configuration.GetSection(ActivationConfig.Key));
        services.Configure<PasswordResetConfig>(configuration.GetSection(PasswordResetConfig.Key));
        services.Configure<BrevoConfig>(configuration.GetSection(BrevoConfig.Key));


        return services;
    }
}
