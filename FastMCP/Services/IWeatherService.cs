using FastMCP.Services.Requests;

namespace FastMCP.Services;

public interface IWeatherService
{
    /// <summary>
    /// Gets the current weather in the specific location
    /// </summary>
    /// <param name="request">Location and units</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Response with all available data</returns>
    /// <exception cref="InvalidOperationException">If city was not provided of failed to receive city coordinates</exception>
    ValueTask<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeather request,  CancellationToken token);
    
    /// <summary>
    /// Gets the weather forecast in the specific location
    /// </summary>
    /// <param name="request">Location and units</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Response with all available data</returns>
    /// <exception cref="InvalidOperationException">If city was not provided of failed to receive city coordinates</exception>
    ValueTask<GetWeatherForecastResponse> GetForecastAsync(GetWeatherForecast request, CancellationToken token);
    
    /// <summary>
    /// Gets the weather forecast in the specific location
    /// </summary>
    /// <param name="request">Location</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Response with all available data</returns>
    /// <exception cref="InvalidOperationException">If city was not provided of failed to receive city coordinates</exception>
    ValueTask<GetWeatherAlertsResponse> GetWeatherAlertsAsync(GetWeatherAlerts request, CancellationToken token);
}