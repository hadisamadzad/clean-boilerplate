using System;
using Communal.Application.Configurations;
using Identity.ServiceBus.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions.DependencyInjection
{
    public static class MassTransitInjection
    {
        public static IServiceCollection AddConfiguredMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitConfig = configuration.GetSection(RabbitMQConfig.Key).Get<RabbitMQConfig>();

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumer<GetUserBusRequestConsumer>();

                x.UsingRabbitMq((context, config) =>
                {
                    config.UseHealthCheck(context);
                    config.Host(new Uri(rabbitConfig.Host), h =>
                        {
                            h.Username(rabbitConfig.Username);
                            h.Password(rabbitConfig.Password);
                        }
                    );
                    config.ConfigureEndpoints(context);
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}