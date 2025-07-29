using FastMCP.HttpClients.Weather.Requests;

namespace FastMCP.Tests;

public static partial class Utils
{
   public static class GetLocation
   {
      public static GetLocationResponseItem Create()
      {
         return new GetLocationResponseItem
         {
            Country = "test country",
            Latitude = 100,
            Longitude = 100,
            Name = "test name",
            State = "test state"
         };
      }
   } 
}