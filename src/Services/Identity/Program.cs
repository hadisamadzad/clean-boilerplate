using System.Text.Json.Serialization;
using Identity.App.DependencyInjection;
using Identity.App.Middleware;
using Identity.Application.Interfaces;
using Identity.Database;
using Identity.Database.Seeding;
using Identity.Infrastructure;
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
    ApplicationName = "Identity",
    EnvironmentName = env
});

builder.Configuration.AddConfiguration(configs);
builder.Logging.ClearProviders();
builder.Host.UseSerilog();

// Add services to the container
builder.Services
    .AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddConfigurations(configs);
builder.Services.AddCors(options => options
    .AddPolicy("general", policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));
builder.Services.AddConfiguredMediatR();
builder.Services.AddConfiguredDatabase(configs);
//builder.Services.AddConfiguredRedisCache(configs);

builder.Services.AddSingleton<ITransactionalEmailService, BrevoEmailService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddConfiguredHttpClient(configs);

builder.Services.AddHealthChecks();
builder.Services.AddConfiguredSwagger();

WebApplication app = null;
try { app = builder.Build(); }
catch (Exception ex) { Log.Fatal(ex, "Application failed to build."); }

// Add middleware
app.UseCors("general");
app.MapHealthChecks("/health");
app.MapControllers();

if (!app.Environment.IsProduction())
    app.UseConfiguredSwagger();

MigrationRunner.Run(app.Services);
Seeder.Seed(app.Services);

try { app.Run(); }
catch (Exception ex) { Log.Fatal(ex, "Application failed to start."); }