using Identity.Application.UseCases.Auth;

namespace Identity.Core.Bootstrap;

public static class MediatRServiceExtensions
{
    public static IServiceCollection AddConfiguredMediatR(this IServiceCollection services)
    {
        // Handlers
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining<LoginCommand>());

        return services;
    }
}
