﻿using Identity.Application.Interfaces;
using Identity.Application.Types.Configs;
using Identity.Infrastructure;

namespace Identity.Core.ServiceRegistrations;

public static class HttpClientRegistration
{
    public static IServiceCollection AddConfiguredHttpClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var brevoConfig = configuration.GetSection(BrevoConfig.Key).Get<BrevoConfig>();

        // Brevo client
        services.AddHttpClient<ITransactionalEmailService, BrevoEmailService>((serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(brevoConfig.BaseAddress);
        });

        return services;
    }
}