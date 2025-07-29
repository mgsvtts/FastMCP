using FastMCP.HttpClients.Weather;
using FastMCP.HttpClients.Weather.Requests;
using FastMCP.Services.Requests;
using FastMCP.Services.Requests.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace FastMCP.HttpClients.Cache;

public sealed class CachedWeatherHttpClient: IOpenWeatherHttpClient
{
    private readonly IMemoryCache _cache;
    private readonly OpenWeatherHttpClient _decoratee;

    public CachedWeatherHttpClient(IMemoryCache cache, OpenWeatherHttpClient decoratee)
    {
        _cache = cache;
        _decoratee = decoratee;
    }

    public Task<GetLocationResponseItem> GetLocationAsync(string location, CancellationToken token)
    {
        return CacheResponseAsync(location, () => _decoratee.GetLocationAsync(location, token));
    }

    public Task<GetWeatherResponse> GetWeatherAsync(double latitude, double longitude, Units units, CancellationToken token)
    {
        return CacheResponseAsync
        (
            $"{latitude}:{longitude}:{units}",
            () => _decoratee.GetWeatherAsync(latitude, longitude, units, token)
        );
    }

    private async Task<T> CacheResponseAsync<T>(string key, Func<Task<T>> func)
    {
        if (_cache.TryGetValue(key, out T? item))
        {
            return item;
        }
        
        item = await func();
        
        _cache.Set(key, item, TimeSpan.FromMinutes(15));

        return item;
    }
}