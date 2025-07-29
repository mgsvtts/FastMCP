using System.ComponentModel;
using FastMCP.HttpClients.Weather;
using FastMCP.Services;
using FastMCP.Services.Requests;
using FastMCP.Services.Requests.Dto;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace FastMCP.Tools;

[McpServerToolType]
public sealed class WeatherTools
{
    private readonly IWeatherService _service;
    private readonly ILogger<WeatherTools> _logger;

    public WeatherTools(IWeatherService service, ILogger<WeatherTools> logger)
    {
        _service = service;
        _logger = logger;
    }

    [McpServerTool]
    [Description("Describes weather in the provided city.")]
    public async ValueTask<ToolResponse<GetCurrentWeatherResponse>> GetCurrentWeather(
        [Description("Name of the city to return weather for")]
        string city,
        [Description("Units of weather measurement")]
        Units units,
        CancellationToken token)
    {
        try
        {
            return await _service.GetCurrentWeatherAsync(new GetCurrentWeather
            {
                City = city,
                Units = units
            }, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {MethodName}", nameof(GetCurrentWeather));

            return ToolResponse.Error<GetCurrentWeatherResponse>(
                $"Failed to generate weather response for the city: {ex.Message}");
        }
    }
    
    [McpServerTool]
    [Description("Describes weather in the provided city.")]
    public async ValueTask<ToolResponse<GetWeatherForecastResponse>> GetWeatherForecast(
        [Description("Name of the city to return weather for")]
        string city,
        [Description("Units of weather measurement")]
        Units units,
        CancellationToken token)
    {
        try
        {
            return await _service.GetForecastAsync(new GetWeatherForecast
            {
                City = city,
                Units = units
            }, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {MethodName}", nameof(GetWeatherForecast));

            return ToolResponse.Error<GetWeatherForecastResponse>(
                $"Failed to generate weather response for the city: {ex.Message}");
        }
    }
    
    [McpServerTool]
    [Description("Describes weather alerts in the provided city.")]
    public async ValueTask<ToolResponse<GetWeatherAlertsResponse>> GetWeatherAlerts(
        [Description("Name of the city to return weather for")]
        string city,
        CancellationToken token)
    {
        try
        {
            return await _service.GetWeatherAlertsAsync(new GetWeatherAlerts
            {
                City = city
            }, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {MethodName}", nameof(GetWeatherAlerts));

            return ToolResponse.Error<GetWeatherAlertsResponse>(
                $"Failed to generate weather response for the city: {ex.Message}");
        }
    }
}