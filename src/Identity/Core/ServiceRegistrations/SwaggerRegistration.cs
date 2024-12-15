using Microsoft.OpenApi.Models;

namespace Identity.Core.ServiceRegistrations;

public static class SwaggerRegistration
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
                    Title = "Bloggy Identity API",
                    Version = "1"
                });
        });


        return services;
    }

    public static void UseConfiguredSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(configs =>
        {
            configs.DocumentTitle = "Bloggy Identity Swagger UI";
            configs.SwaggerEndpoint("/swagger/v1/swagger.json", "Bloggy Identity API");
        });
    }
}
