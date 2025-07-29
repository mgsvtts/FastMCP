using FastMCP.HttpClients.Weather;
using FastMCP.HttpClients.Weather.Requests;
using FastMCP.Services.Requests;
using FastMCP.Services.Requests.Dto;
using FastMCP.Tools;
using Microsoft.Extensions.Logging;

namespace FastMCP.Services;

public class WeatherService : IWeatherService
{
    private readonly IOpenWeatherHttpClient _httpClient;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(IOpenWeatherHttpClient httpClient, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async ValueTask<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeather request, CancellationToken token)
    {
        var weather = await GetWeatherAsync(request.City, request.Units, token);
        
        return weather.ToCurrentWeather(request.City);
    }
    
    public async ValueTask<GetWeatherForecastResponse> GetForecastAsync(GetWeatherForecast request, CancellationToken token)
    {
        var weather = await GetWeatherAsync(request.City, request.Units, token);
        
        return weather.ToWeatherForecast(request.City);
    }
    
    public async ValueTask<GetWeatherAlertsResponse> GetWeatherAlertsAsync(GetWeatherAlerts request, CancellationToken token)
    {
        var weather = await GetWeatherAsync(request.City, Units.Metric, token);
        
        var response = weather.ToWeatherAlerts(request.City);

        if (response.WeatherAlerts is null || response.WeatherAlerts.Count == 0)
        {
            throw new InvalidOperationException($"Weather alerts not found for city: {request.City}");
        }

        return response;
    }

    private async ValueTask<GetWeatherResponse> GetWeatherAsync(string city, Units units, CancellationToken token)
    {
        if (string.IsNullOrEmpty(city))
        {
            _logger.LogWarning("User provided an empty city name");
            
            throw new InvalidOperationException("City name must be provided");
        }

        var location = await GetLocationAsync(city, token);

        return await GetWeatherAsync(units, location, token);
    }

    private async Task<GetWeatherResponse> GetWeatherAsync(Units units, GetLocationResponseItem location, CancellationToken token)
    {
        var weather = await _httpClient.GetWeatherAsync(location.Latitude, location.Longitude, units, token);
        
        _logger.LogDebug("Weather found: {@Location}", location);

        if (weather != default)
        {
            return weather;
        }

        _logger.LogError(
            "Failed to get weather response for the coordinates {Latitude}:{Longitude}",
            location.Latitude,
            location.Longitude
        );

        throw new InvalidOperationException("Failed to find location for the city");
    }

    private async Task<GetLocationResponseItem> GetLocationAsync(string city, CancellationToken token)
    {
        var location = await _httpClient.GetLocationAsync(city.Trim(), token);

        _logger.LogDebug("Location found: {@Location}", location);

        if (location != default)
        {
            return location;
        }

        _logger.LogError("Cannot find location for the city: {CityName}", city);

        throw new InvalidOperationException("Failed to find location for the city");
    }
}