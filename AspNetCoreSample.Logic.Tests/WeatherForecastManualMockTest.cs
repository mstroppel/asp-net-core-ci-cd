using AspNetCoreSample.Outbound;

namespace AspNetCoreSample.Logic.Tests;

public class WeatherForecastManualMockTest
{
    [Fact]
    public void GetForecast_ShouldReturnForecasts()
    {
        // Arrange
        var forecastStorage = new MockForecastStorage();
        var weatherForecastService = new WeatherForecastService(forecastStorage);

        // Act
        var forecasts = weatherForecastService.GetForecasts();

        // Assert
        Assert.Equal(5, forecasts.Count());
    }

    public class MockForecastStorage : IForecastStorage
    {
        public OutboundWeatherForecast GetForecast(DateOnly date)
        {
            return new OutboundWeatherForecast(
                date,
                25,
                "Sunny"
            );
        }
    }
}