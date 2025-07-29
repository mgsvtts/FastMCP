using FastMCP.HttpClients.Weather.Requests;

namespace FastMCP.Tests;

public static partial class Utils
{
   public static class GetWeathers
   {
      public static GetWeatherResponse Create(bool withAlerts = true)
      {
         return new GetWeatherResponse
         {
            CurrentWeather = CurrentWeathers.Create(),
            DailyWeather = DailyWeathers.CreateMany(10),
            Latitude = 100,
            Longitude = 100,
            WeatherAlerts = withAlerts ? WeatherAlerts.CreateMany(10) : []
         };
      }
   }

   public static class CurrentWeathers
   {
      public static CurrentWeather Create()
      {
         return new CurrentWeather
         {
            CloudCoveragePercent = 10,
            DateTimeUnix = 123456789,
            FeelsLike = 15,
            SunriseUnix = 123456789,
            SunsetUnix = 123456789,
            Temperature = 55,
            WindSpeed = 10
         };
      }
   }

   public static class DailyWeathers
   {
      public static List<DailyWeather> CreateMany(int number)
      {
         return Enumerable.Range(0, number).Select(x => new DailyWeather
         {
            CloudCoveragePercent = 10,
            DateTimeUnix = 123456789,
            SunriseUnix = 123456789,
            SunsetUnix = 123456789,
            WindSpeed = 10
         }).ToList();
      }
   }

   public static class WeatherAlerts
   {
      public static List<WeatherAlert> CreateMany(int number)
      {
         return Enumerable.Range(0, number).Select(x => new WeatherAlert()
         {
            SenderName = $"test-sender-name-{x}",
            AlertDescription = $"test-description-{x}",
            EventName = $"test-event-name-{x}",
            StartUnix = 123456789,
            EndUnix = 123456789,
         }).ToList();
      }
   }
}