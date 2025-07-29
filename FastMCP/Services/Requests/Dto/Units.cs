namespace FastMCP.Services.Requests.Dto;

public enum Units
{
    Standard = 0,
    Metric = 1,
    Imperial = 2
}

public static class UnitsExtensions
{
    public static Units ToUnits(this string units)
    {
        return units.Trim().ToLower() switch
        {
            "metric" => Units.Metric,
            "imperial" => Units.Imperial,
            "standard" => Units.Standard,
            _ => Units.Metric,
        };
    }
}