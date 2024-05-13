namespace Identity.App.Middleware;

internal static class ConfiguredSwaggerMiddleware
{
    public static void UseConfiguredSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(configs =>
        {
            configs.DocumentTitle = "Identity Swagger UI";
            configs.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API");
        });
    }
}