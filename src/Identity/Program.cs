using System.Text.Json.Serialization;
using Identity.Api;
using Identity.Application.Interfaces;
using Identity.Core.ServiceRegistrations;
using Identity.Infrastructure.Database;
using Serilog;

// Attain environment name
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

// Build configs reading from appsettings.json
var configs = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .Build();

// Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configs)
    .Enrich.WithMachineName()
    .CreateLogger();

// Create app builder
var builder = WebApplication.CreateBuilder();

// Use Serilog as logging provider
//builder.Logging.ClearProviders();
builder.Host.UseSerilog(Log.Logger);

// Add configs
builder.Configuration.AddConfiguration(configs);


// Configure JSON options to serialize enums as strings
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Configure cors
builder.Services.AddCors(options => options
    .AddPolicy("general", policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));

// Add services to the container
builder.Services.AddCustomConfigurations(configs);
builder.Services.AddControllers();
builder.Services.AddConfiguredMediatR();

builder.Services.AddConfiguredMongoDB(configs);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddConfiguredRedisCache(configs);

//builder.Services.AddSingleton<ITransactionalEmailService, BrevoEmailService>();

builder.Services.AddConfiguredHttpClient(configs);

builder.Services.AddHealthChecks();
builder.Services.AddConfiguredSwagger();

WebApplication app = null;
try
{
    app = builder.Build();
    Log.Information("Application started ...");
}
catch (Exception ex) { Log.Fatal(ex, "Application failed to build."); }

// Add middleware
app.UseCors("general");
app.MapHealthChecks("/health");

// Add endpoints
app.MapAuthEndpoints();
app.MapPasswordResetEndpoints();

if (!app.Environment.IsProduction())
    app.UseConfiguredSwagger();

//MigrationRunner.Run(app.Services);

try { app.Run(); }
catch (Exception ex) { Log.Fatal(ex, "Application failed to start."); }