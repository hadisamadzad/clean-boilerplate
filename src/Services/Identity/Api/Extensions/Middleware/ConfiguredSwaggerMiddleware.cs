using Microsoft.AspNetCore.Builder;

namespace Identity.Extensions.Middleware
{
    internal static class ConfiguredSwaggerMiddleware
    {
        public static void UseConfiguredSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(configs =>
                configs.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API v1"));
        }
    }
}