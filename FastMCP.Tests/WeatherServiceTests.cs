using FastMCP.HttpClients.Weather;
using FastMCP.HttpClients.Weather.Requests;
using FastMCP.Services;
using FastMCP.Services.Requests;
using FastMCP.Services.Requests.Dto;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FastMCP.Tests;

public sealed class WeatherServiceTests
{
    private readonly WeatherService _weatherService;
    private readonly IOpenWeatherHttpClient _httpClient;
    
    public WeatherServiceTests()
    {
        _httpClient = Substitute.For<IOpenWeatherHttpClient>();

        _weatherService = new WeatherService(_httpClient, Substitute.For<ILogger<WeatherService>>());
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task GetCurrentWeatherAsync_ThrowsInvalidOperationException_IfNoCityProvided(string? city)
    {
        var func = async () => await _weatherService.GetCurrentWeatherAsync(new GetCurrentWeather
        {
            City = city!,
            Units = Units.Metric
        }, default);

        var exception = await Record.ExceptionAsync(func);
        
        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Contains("City name must be provided", exception.Message);
    }
    
    [Fact]
    public async Task GetCurrentWeatherAsync_ThrowsInvalidOperationException_IfFailedToGetLocationCoordinates()
    {
        _httpClient.GetLocationAsync(Arg.Any<string>(), default)
            .Returns(new GetLocationResponseItem());
        
        var func = async () => await _weatherService.GetCurrentWeatherAsync(new GetCurrentWeather
        {
            City = "mock-city",
            Units = Units.Metric
        }, default);

        var exception = await Record.ExceptionAsync(func);
        
        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Contains("Failed to find location for the city", exception.Message);
    }
    
    [Fact]
    public async Task GetWeatherAlertsAsync_ThrowsInvalidOperationException_IfNoAlertsFound()
    {
        _httpClient.GetLocationAsync(Arg.Any<string>(), default)
            .Returns(Utils.GetLocation.Create());
        
        _httpClient.GetWeatherAsync(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<Units>(), default)
            .Returns(Utils.GetWeathers.Create(withAlerts: false));

        var func = async () => await _weatherService.GetWeatherAlertsAsync(new GetWeatherAlerts
        {
            City = "mock-city",
        }, default);

        var exception = await Record.ExceptionAsync(func);
        
        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Contains("Weather alerts not found for city", exception.Message);
    }}