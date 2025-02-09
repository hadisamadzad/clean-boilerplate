using Identity.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api;

public static class DevEndpoints
{
    const string Route = "api/dev/";
    const string Tag = "Dev";

    // Endpoints
    public static void MapDevEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(Route).WithTags(Tag);

        // Test logger
        group.MapGet("logger/test", (
            ILogger<object> logger,
            [FromQuery] string message) =>
            {
                logger.LogInformation("Hey, we have got a log: {message}", message);
                return Results.Ok(true);
            });

        // Test email service
        group.MapGet("email", async (
            ITransactionalEmailService emailService) =>
            {
                var parameters = new Dictionary<string, string>
                {
                    { "Link", "https://hadisamadzad.com" }
                };

                _ = await emailService
                    .SendEmailByTemplateIdAsync(1, ["h.samadzad@gmail.com"], parameters);

                return Results.Ok("Email sent");
            });

    }
}