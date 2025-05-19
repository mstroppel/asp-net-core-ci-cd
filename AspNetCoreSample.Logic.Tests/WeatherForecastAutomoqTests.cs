using AspNetCoreSample.Outbound;
using FluentAssertions;
using Moq;

namespace AspNetCoreSample.Logic.Tests;

public class WeatherForecastAutomoqTests
{
    [Fact]
    public void GetForecast_ShouldReturnForecasts()
    {
        var forecastStorageMock = new Mock<IForecastStorage>();
        forecastStorageMock
            .Setup(x => x.GetForecast(It.IsAny<DateOnly>()))
            .Returns(new OutboundWeatherForecast(DateOnly.FromDateTime(DateTime.Now), 27, "Cloudy"));
        var weatherForecastService = new WeatherForecastService(forecastStorageMock.Object);
        
        var forecasts = weatherForecastService.GetForecasts();
        
        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(2, forecasts.Count());
        
        var forecast = forecasts.First();
        Assert.Equal(DateOnly.FromDateTime(DateTime.Now), forecast.Date);
        Assert.Equal(27, forecast.TemperatureC);
        Assert.Equal("Cloudy", forecast.Summary);
    }
}