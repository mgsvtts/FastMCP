namespace FastMCP.Services.Requests;

public readonly record struct GetWeatherAlerts
{
    public required string City { get; init; }
}

public sealed class GetWeatherAlertsResponse
{
    public required string City { get; init; }
    public IReadOnlyCollection<Alert>? WeatherAlerts { get; init; }
}

public sealed class Alert
{
    public required string? Sender { get; init; }
    public required DateTime StartUtc { get; init; }
    public required DateTime EndUtc { get; init; }
    public required string? Description { get; init; }
}