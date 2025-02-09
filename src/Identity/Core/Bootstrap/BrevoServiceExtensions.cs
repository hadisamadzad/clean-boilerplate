using Identity.Application.Interfaces;
using Identity.Application.Types.Configs;
using Identity.Infrastructure;

namespace Identity.Core.Bootstrap;

public static class BrevoServiceExtensions
{
    public static IServiceCollection AddConfiguredBrevo(this IServiceCollection services,
        IConfiguration configuration)
    {
        var brevoConfig = configuration.GetSection(BrevoConfig.Key).Get<BrevoConfig>();
        services.Configure<BrevoConfig>(configuration.GetSection(BrevoConfig.Key));

        // Brevo client
        services.AddHttpClient<IEmailService, BrevoEmailService>((serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(brevoConfig.BaseAddress);
        });

        return services;
    }
}