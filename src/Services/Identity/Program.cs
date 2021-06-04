using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Identity
{
    public class Program
    {
        private static readonly string ServiceName = typeof(Program).Namespace?.Split('.')[0];
        private static readonly string Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        private static readonly IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Env}.json")
            .AddEnvironmentVariables()
            .Build();

        // Main
        public static void Main(string[] args)
        {
            // Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.WithMachineName()
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureLogging(loggingConfiguration => loggingConfiguration.ClearProviders())
            .UseConfiguration(Configuration)
            .UseStartup<Startup>();
    }
}