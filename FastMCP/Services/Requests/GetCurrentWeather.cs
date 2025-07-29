using FastMCP.Services.Requests.Dto;

namespace FastMCP.Services.Requests;

public readonly record struct GetCurrentWeather
{
   public required Units Units { get; init; } 
   public required string City { get; init; }
}

public sealed class GetCurrentWeatherResponse
{
   public required string City { get; init; }
   public required double Temperature { get; init; }
   public required double FeelsLike { get; init; }
   public required DateTime SunriseUtc { get; init; }
   public required DateTime SunsetUtc { get; init; }
   public required int CloudCoveragePercent { get; init; }
   public required double WindSpeed { get; init; }
}

