using Microsoft.OpenApi.Models;

namespace Identity.App.DependencyInjection;

public static class SwaggerInjection
{
    public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(configs =>
        {
            configs.SwaggerDoc(
                name: "v1",
                info: new OpenApiInfo
                {
                    Title = "Identity API",
                    Version = "1"
                });
        });


        return services;
    }
}
