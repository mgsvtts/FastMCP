namespace FastMCP.HttpClients.Weather.Options;

public sealed class OpenWeatherOptions
{
    public required string ApiKey { get; init; }
    public required string Url { get; init; }
}