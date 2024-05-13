using System.Text.Json.Serialization;
using Gateway;
using Gateway.DependencyInjection;
using Gateway.Middleware;
using Ocelot.DependencyInjection;
using Serilog;

var env = Environment
    .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

var configs = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .AddEnvironmentVariables()
    .Build();

// Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configs)
    .Enrich.WithMachineName()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = "Gateway",
    EnvironmentName = env
});

builder.Configuration.AddConfiguration(configs);
builder.Logging.ClearProviders();
builder.Host.UseSerilog();
builder.Configuration.AddOcelot(Constants.RouteConfigPath, builder.Environment);

// Add services to the container
builder.Services
    .AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddConfiguredCors(configs);
builder.Services.AddConfiguredAuthentication(configs);
builder.Services.AddConfiguredOcelot();

builder.Services.AddHealthChecks();

WebApplication app = null;
try { app = builder.Build(); }
catch (Exception ex) { Log.Fatal(ex, "Application failed to build."); }
finally { Log.CloseAndFlush(); }

// Add middleware

//if (builder.Environment.IsProduction())
//    app.UseHsts();

app.UseCors(Constants.CorsPolicyName);
app.UseHealthChecks("/health");

app.UseConfiguredOcelot();

try { app.Run(); }
catch (Exception ex) { Log.Fatal(ex, "Application failed to start."); }
finally { Log.CloseAndFlush(); }