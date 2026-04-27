using AspNetCoreSample.Outbound;
using FluentAssertions;

namespace AspNetCoreSample.Logic.Tests;

public class WeatherForecastManualMockTest
{
    [Fact]
    public void GetForecast_ShouldReturnForecasts()
    {
        var uut = new WeatherForecastService(new ForecastStorageMock());

        var actual = uut.GetForecasts();

        actual.Should().HaveCount(5);
    }

    [Fact]
    public void GetTomorrowForecast_ShouldReturnTomorrowsForecast()
    {
        var uut = new WeatherForecastService(new ForecastStorageMock());

        var actual = uut.GetTomorrowForecast();

        actual.Date.Should().Be(DateOnly.FromDateTime(DateTime.Now.AddDays(1)));
        actual.TemperatureC.Should().Be(25);
        actual.Summary.Should().Be("Sunny");
    }

    public class ForecastStorageMock : IForecastStorage
    {
        public OutboundWeatherForecast GetForecast(DateOnly date)
        {
            return new OutboundWeatherForecast(date, 25, "Sunny");
        }
    }
}