using FastMCP.Services.Requests;

namespace FastMCP.HttpClients.Weather.Requests;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public readonly record struct GetWeatherResponse
{
    [JsonPropertyName("lat")]
    public double Latitude { get; init; }
    
    [JsonPropertyName("lon")]
    public double Longitude { get; init; }
    
    [JsonPropertyName("current")]
    public CurrentWeather CurrentWeather { get; init; }
    
    [JsonPropertyName("daily")]
    public List<DailyWeather> DailyWeather { get; init; }
    
    [JsonPropertyName("alerts")]
    public List<WeatherAlert> WeatherAlerts { get; init; }

    public GetCurrentWeatherResponse ToCurrentWeather(string city)
    {
        return new GetCurrentWeatherResponse
        {
            City = city,
            CloudCoveragePercent = CurrentWeather.CloudCoveragePercent,
            FeelsLike = CurrentWeather.FeelsLike,
            SunriseUtc = DateTimeOffset.FromUnixTimeSeconds(CurrentWeather.SunriseUnix).UtcDateTime,
            SunsetUtc = DateTimeOffset.FromUnixTimeSeconds(CurrentWeather.SunsetUnix).UtcDateTime,
            Temperature = CurrentWeather.Temperature,
            WindSpeed = CurrentWeather.WindSpeed
        };
    }

    public GetWeatherForecastResponse ToWeatherForecast(string city)
    {
        return new GetWeatherForecastResponse
        {
            City = city,
            Forecasts = DailyWeather.Select(x => new WeatherForecastItem
            {
                Temperature = x.Temperature?.ToTemperature(),
                FeelsLike = x.FeelsLikeTemperature?.ToFeelsLike(),
                SunriseUtc = DateTimeOffset.FromUnixTimeSeconds(x.SunriseUnix).UtcDateTime,
                SunsetUtc = DateTimeOffset.FromUnixTimeSeconds(x.SunsetUnix).UtcDateTime,
                MoonriseUtc = DateTimeOffset.FromUnixTimeSeconds(x.MoonriseUnix).UtcDateTime,
                MoonsetUtc = DateTimeOffset.FromUnixTimeSeconds(x.MoonsetUnix).UtcDateTime,
                Summary = x.Summary,
                WindSpeed = x.WindSpeed,
                CloudCoveragePercent = x.CloudCoveragePercent,
            }).ToList()
        };
    }

    public GetWeatherAlertsResponse ToWeatherAlerts(string city)
    {
        return new GetWeatherAlertsResponse
        {
            City = city,
            WeatherAlerts = WeatherAlerts?.Select(x => new Alert
            {
                Description = x.AlertDescription,
                Sender = x.SenderName,
                StartUtc = DateTimeOffset.FromUnixTimeSeconds(x.StartUnix).UtcDateTime,
                EndUtc = DateTimeOffset.FromUnixTimeSeconds(x.EndUnix).UtcDateTime,
            }).ToList()
        };
    }
}

public readonly record struct CurrentWeather
{
    [JsonPropertyName("dt")]
    public long DateTimeUnix { get; init; }
    
    [JsonPropertyName("sunrise")]
    public long SunriseUnix { get; init; }
    
    [JsonPropertyName("sunset")]
    public long SunsetUnix { get; init; }
    
    [JsonPropertyName("temp")]
    public double Temperature { get; init; }
    
    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; init; }
    
    [JsonPropertyName("clouds")]
    public int CloudCoveragePercent { get; init; }
    
    [JsonPropertyName("wind_speed")]
    public double WindSpeed { get; init; }
}

public readonly record struct DailyWeather
{
    [JsonPropertyName("dt")]
    public long DateTimeUnix { get; init; }
    
    [JsonPropertyName("sunrise")]
    public long SunriseUnix { get; init; }
    
    [JsonPropertyName("sunset")]
    public long SunsetUnix { get; init; }
    
    [JsonPropertyName("moonrise")]
    public long MoonriseUnix { get; init; }
    
    [JsonPropertyName("moonset")]
    public long MoonsetUnix { get; init; }
    
    [JsonPropertyName("summary")]
    public string? Summary { get; init; }
    
    [JsonPropertyName("temp")]
    public DailyTemperature? Temperature { get; init; }
    
    [JsonPropertyName("feels_like")]
    public DailyFeelsLike? FeelsLikeTemperature { get; init; }
    
    [JsonPropertyName("wind_speed")]
    public double WindSpeed { get; init; }
    
    [JsonPropertyName("clouds")]
    public int CloudCoveragePercent { get; init; }
}

public readonly record struct DailyTemperature
{
    [JsonPropertyName("day")]
    public double Daytime { get; init; }
    
    [JsonPropertyName("min")]
    public double Minimum { get; init; }
    
    [JsonPropertyName("max")]
    public double Maximum { get; init; }
    
    [JsonPropertyName("night")]
    public double Nighttime { get; init; }
    
    [JsonPropertyName("eve")]
    public double Evening { get; init; }
    
    [JsonPropertyName("morn")]
    public double Morning { get; init; }

    public Temperature ToTemperature()
    {
        return new Temperature
        {
            Daytime = Daytime,
            Minimum = Minimum,
            Maximum = Maximum,
            Nighttime = Nighttime,
            Evening = Evening,
            Morning = Morning,
        };
    }
}

public readonly record struct DailyFeelsLike
{
    [JsonPropertyName("day")]
    public double DaytimeFeelsLike { get; init; }
    
    [JsonPropertyName("night")]
    public double NighttimeFeelsLike { get; init; }
    
    [JsonPropertyName("eve")]
    public double EveningFeelsLike { get; init; }
    
    [JsonPropertyName("morn")]
    public double MorningFeelsLike { get; init; }

    public FeelsLike ToFeelsLike()
    {
        return new FeelsLike
        {
            Daytime = DaytimeFeelsLike,
            Nighttime = NighttimeFeelsLike,
            Evening = EveningFeelsLike,
            Morning = MorningFeelsLike
        };
    }
}

public readonly record struct WeatherAlert
{
    [JsonPropertyName("sender_name")]
    public string? SenderName { get; init; }
    
    [JsonPropertyName("event")]
    public string? EventName { get; init; }
    
    [JsonPropertyName("start")]
    public long StartUnix { get; init; }
    
    [JsonPropertyName("end")]
    public long EndUnix { get; init; }
    
    [JsonPropertyName("description")]
    public string? AlertDescription { get; init; }
    
    [JsonPropertyName("tags")]
    public List<string>? AlertTags { get; init; }
}