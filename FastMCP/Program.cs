using FastMCP.HttpClients.Weather;
using FastMCP.HttpClients.Weather.Options;
using FastMCP.Services;
using FastMCP.HttpClients.Cache;
using Microsoft.Extensions.Http.Resilience;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole(consoleLogOptions =>
{
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithHttpTransport(o => o.Stateless = true)
    .WithToolsFromAssembly();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient("default").AddResilienceHandler("retry", x =>
{
    x.AddRetry(new HttpRetryStrategyOptions
    {
        MaxRetryAttempts = 5,
        Delay = TimeSpan.FromSeconds(2),
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = true,
    });
});

builder.Services.AddScoped<IOpenWeatherHttpClient, CachedWeatherHttpClient>();
builder.Services.AddScoped<OpenWeatherHttpClient>();

builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.Configure<OpenWeatherOptions>(builder.Configuration.GetSection("WeatherApi"));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapMcp("/mcp");

app.Run();