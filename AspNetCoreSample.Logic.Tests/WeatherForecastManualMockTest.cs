using AspNetCoreSample.Outbound;

namespace AspNetCoreSample.Logic.Tests;

public class WeatherForecastManualMockTest
{
    [Fact]
    public void GetForecast_ShouldReturnForecasts()
    {
        // Arrange
        var forecastStorage = new TestForecastStorage();
        var weatherForecastService = new WeatherForecastService(forecastStorage);
        
        // Act
        var forecasts = weatherForecastService.GetForecasts();
        
        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(2, forecasts.Count());
        
        var forecast = forecasts.First();
        Assert.Equal(DateOnly.FromDateTime(DateTime.Now), forecast.Date);
        Assert.Equal(25, forecast.TemperatureC);
        Assert.Equal("Sunny", forecast.Summary);
    }
    
    private class TestForecastStorage : IForecastStorage
    {
        public OutboundWeatherForecast GetForecast(DateOnly date)
        {
            return new OutboundWeatherForecast(
                date,
                25,
                "Sunny");
        }
    }
}