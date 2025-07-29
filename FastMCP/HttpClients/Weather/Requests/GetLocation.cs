using System.Text.Json.Serialization;

namespace FastMCP.HttpClients.Weather.Requests;

public readonly record struct GetLocationResponseItem
{
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("lat")]
    public double Latitude { get; init; }
    
    [JsonPropertyName("lon")]
    public double Longitude { get; init; }
    
    [JsonPropertyName("country")]
    public string Country { get; init; }
    
    [JsonPropertyName("state")]
    public string State { get; init; }
}