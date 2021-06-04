using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Identity.Extensions.Middleware
{
    internal static class ConfiguredExceptionHandlerMiddleware
    {
        public static void UseConfiguredExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
                errorApp.Run(async context =>
                {
                    const HttpStatusCode responseStatus = HttpStatusCode.InternalServerError;
                    context.Response.StatusCode = (int)responseStatus;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (error == null)
                        return;

                    string json;
                    if (env.IsProduction())
                        json = JsonConvert.SerializeObject(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Title = responseStatus.ToString(),
                        });
                    else
                        json = JsonConvert.SerializeObject(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Title = responseStatus.ToString(),
                            Message = error.Message,
                            StackTrace = error.StackTrace
                        });

                    await context.Response.WriteAsync(json);
                })
            );
        }
    }
}