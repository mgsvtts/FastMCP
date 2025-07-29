using FastMCP.Services.Requests.Dto;

namespace FastMCP.Services.Requests;

public readonly record struct GetWeatherForecast
{
    public required Units Units { get; init; } 
    public required string City { get; init; }
}

public sealed class GetWeatherForecastResponse
{
    public required string City { get; init; }
    public required IReadOnlyCollection<WeatherForecastItem> Forecasts { get; init; }
}

public sealed class WeatherForecastItem
{
   public required DateTime SunriseUtc { get; init; } 
   public required DateTime SunsetUtc { get; init; } 
   public required DateTime MoonriseUtc { get; init; } 
   public required DateTime MoonsetUtc { get; init; } 
   public required Temperature? Temperature { get; init; }
   public required FeelsLike? FeelsLike { get; init; }
   public required double WindSpeed { get; init; }
   public required int CloudCoveragePercent { get; init; }
   public required string? Summary { get; init; }
}

public sealed class FeelsLike
{
    public required double Daytime { get; init; }
    
    public required double Nighttime { get; init; }
    
    public required double Evening { get; init; }
    
    public required double Morning { get; init; }
}

public sealed class Temperature
{
    public required double Daytime { get; init; }
    
    public required double Minimum { get; init; }
    
    public required double Maximum { get; init; }
    
    public required double Nighttime { get; init; }
    
    public required double Evening { get; init; }
    
    public required double Morning { get; init; }
}