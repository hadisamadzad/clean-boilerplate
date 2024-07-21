using Identity.Application.Operations.Auth;

namespace Identity.App.Registrations;

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
