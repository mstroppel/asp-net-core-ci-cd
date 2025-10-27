using AspNetCoreSample.Outbound;
using FluentAssertions;
using Moq;

namespace AspNetCoreSample.Logic.Tests;

public class WeatherForecastAutomoqTests
{
    [Fact]
    public void GetForecast_ShouldReturnForecasts()
    {
        var mock = new Mock<IForecastStorage>();
        var sut = new WeatherForecastService(mock.Object);
        mock.Setup(x => x.GetForecast(It.IsAny<DateOnly>()))
            .Returns(new OutboundWeatherForecast(DateOnly.FromDateTime(DateTime.Now), 12, "nice"));

        var forecasts = sut.GetForecasts();

        forecasts.Should().HaveCount(5);
    }
}