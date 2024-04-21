using AspNetCoreSample.Outbound;
using FluentAssertions;
using Moq;

namespace AspNetCoreSample.Logic.Tests;

public class WeatherForecastAutomoqTests
{
    [Fact]
    public void GetForecast_ShouldReturnForecasts()
    {
        // Arrange
        var forecastStorageMock = new Mock<IForecastStorage>();
        var forecastStorage = forecastStorageMock.Object;
        var weatherForecastService = new WeatherForecastService(forecastStorage);

        // Act
        var forecasts = weatherForecastService.GetForecasts();

        // Assert
        forecasts.Should().NotBeNull();
        forecasts.Should().HaveCount(5);
    }
}