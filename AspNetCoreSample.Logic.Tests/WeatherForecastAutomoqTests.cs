using AspNetCoreSample.Outbound;
using FluentAssertions;
using Moq;

namespace AspNetCoreSample.Logic.Tests;

public class WeatherForecastAutomoqTests
{
    [Fact]
    public void GetForecast_ShouldReturnForecasts()
    {
        var storageMock = new Mock<IForecastStorage>();

        storageMock.Setup(s => s.GetForecast(It.IsAny<DateOnly>()))
            .Returns((DateOnly date) => new OutboundWeatherForecast(date, 25, "Sunny"));

        var uut = new WeatherForecastService(storageMock.Object);

        var actual = uut.GetForecasts();

        actual.Should().HaveCount(5);
    }

    [Fact]
    public void GetTomorrowForecast_ShouldReturnTomorrowsForecast()
    {
        var tomorrow = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        var storageMock = new Mock<IForecastStorage>();

        storageMock.Setup(s => s.GetForecast(tomorrow))
            .Returns(new OutboundWeatherForecast(tomorrow, 20, "Cloudy"));

        var uut = new WeatherForecastService(storageMock.Object);

        var actual = uut.GetTomorrowForecast();

        actual.Date.Should().Be(tomorrow);
        actual.TemperatureC.Should().Be(20);
        actual.Summary.Should().Be("Cloudy");
    }
}