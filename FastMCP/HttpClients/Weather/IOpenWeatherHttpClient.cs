using System.Runtime.Serialization;
using FastMCP.HttpClients.Weather.Requests;
using FastMCP.Services.Requests;
using FastMCP.Services.Requests.Dto;

namespace FastMCP.HttpClients.Weather;

public interface IOpenWeatherHttpClient
{
    /// <summary>
    /// Get coordinates of desired location (city, state code, country code)
    /// </summary>
    /// <param name="location">Desired location</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Found location</returns>
    /// <exception cref="HttpRequestException">Http request failed with unsuccessful status code</exception>
    /// <exception cref="SerializationException">Failed to deserialize response from the api</exception>
    Task<GetLocationResponseItem> GetLocationAsync(string location, CancellationToken token);

    /// <summary>
    /// Gets weather by coordinates
    /// </summary>
    /// <param name="latitude">Pretty straight forward</param>
    /// <param name="longitude">Pretty straight forward</param>
    /// <param name="units">Units of measurements in the response</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>All accessible data in this location</returns>
    /// <exception cref="HttpRequestException">Http request failed with unsuccessful status code</exception>
    /// <exception cref="SerializationException">Failed to deserialize response from the api</exception>
    Task<GetWeatherResponse> GetWeatherAsync(double latitude, double longitude, Units units, CancellationToken token);
}