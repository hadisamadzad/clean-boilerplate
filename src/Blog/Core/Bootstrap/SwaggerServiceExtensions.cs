using Microsoft.OpenApi.Models;

namespace Blog.Core.Bootstrap;

public static class SwaggerServiceExtensions
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
                    Title = "Bloggy Blog API",
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
            configs.DocumentTitle = "Bloggy Blog Swagger UI";
            configs.SwaggerEndpoint("/swagger/v1/swagger.json", "Bloggy Blog API");
        });
    }
}
