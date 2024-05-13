using Identity.Application.Operations.Auth;

namespace Identity.App.DependencyInjection;

public static class MediatRInjection
{
    public static IServiceCollection AddConfiguredMediatR(this IServiceCollection services)
    {
        // Handlers
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining<LoginCommand>());

        return services;
    }
}
