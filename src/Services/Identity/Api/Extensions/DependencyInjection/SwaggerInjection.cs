using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Identity.Extensions.DependencyInjection
{
    public static class SwaggerInjection
    {
        public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(configs =>
                configs.SwaggerDoc(
                    name: "v1",
                    info: new OpenApiInfo
                    {
                        Title = "Identity Service Api",
                        Version = "v1"
                    }));

            return services;
        }
    }
}