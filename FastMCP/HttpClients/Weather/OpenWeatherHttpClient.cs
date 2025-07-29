using System.Globalization;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Text.Json;
using FastMCP.HttpClients.Weather.Options;
using FastMCP.HttpClients.Weather.Requests;
using FastMCP.Services.Requests;
using FastMCP.Services.Requests.Dto;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace FastMCP.HttpClients.Weather;

public sealed class OpenWeatherHttpClient : IOpenWeatherHttpClient
{
   private readonly HttpClient _client;
   private readonly string _apiKey;
   private readonly string _url;

   public OpenWeatherHttpClient(HttpClient client, IOptionsMonitor<OpenWeatherOptions> options)
   {
      _client = client;
      _apiKey = options.CurrentValue.ApiKey;
      _url = options.CurrentValue.Url;
   }
   
   public async Task<GetLocationResponseItem> GetLocationAsync(string location, CancellationToken token)
   {
      var request = new Dictionary<string, string?>
      {
         ["q"] = location,
         ["limit"] = "1",
         ["appid"] = _apiKey
      };

      var path = QueryHelpers.AddQueryString($"{_url}/geo/1.0/direct", request);
      var response = await _client.GetAsync(path, token);
      
      response.EnsureSuccessStatusCode();

      var locations = await response.Content.ReadFromJsonAsync<IReadOnlyList<GetLocationResponseItem>>(cancellationToken: token)
             ?? throw new SerializationException("Failed to deserialize response from the direct api");
      
      return locations[0];
   }

   public async Task<GetWeatherResponse> GetWeatherAsync(
      double latitude,
      double longitude,
      Units units,
      CancellationToken token)
   {
      var request = new Dictionary<string, string?>
      {
         ["lat"] = latitude.ToString(CultureInfo.InvariantCulture),
         ["lon"] = longitude.ToString(CultureInfo.InvariantCulture),
         ["units"] = units.ToString().ToLower(),
         ["exclude"] = "minutely,hourly",
         ["appid"] = _apiKey,
      };

      var path = QueryHelpers.AddQueryString($"{_url}/data/3.0/onecall", request);
      var response = await _client.GetAsync(path, token);

      response.EnsureSuccessStatusCode();

      return await response.Content.ReadFromJsonAsync<GetWeatherResponse>(cancellationToken: token);
   }
}