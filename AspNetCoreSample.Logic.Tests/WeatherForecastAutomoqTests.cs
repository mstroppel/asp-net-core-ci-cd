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
}