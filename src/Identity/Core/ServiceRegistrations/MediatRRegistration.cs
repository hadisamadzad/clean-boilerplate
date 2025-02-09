using Identity.Application.UseCases.Auth;

namespace Identity.Core.ServiceRegistrations;

public static class MediatRRegistration
{
    public static IServiceCollection AddConfiguredMediatR(this IServiceCollection services)
    {
        // Handlers
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining<LoginCommand>());

        return services;
    }
}
