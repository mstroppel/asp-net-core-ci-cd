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
            .Returns(new OutboundWeatherForecast(DateOnly.FromDateTime(DateTime.Now), 20, "Sunny"));

        var systemUnderTest = new WeatherForecastService(forecastStorageMock.Object);

        var forecasts = systemUnderTest.GetForecasts();

        forecasts.Should().NotBeNull();
        forecasts.Should().HaveCount(6);
    }
}