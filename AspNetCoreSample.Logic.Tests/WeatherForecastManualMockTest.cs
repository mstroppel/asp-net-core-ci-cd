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

        actual.Should().HaveCount(6);
    }

    public class ForecastStorageMock : IForecastStorage
    {
        public OutboundWeatherForecast GetForecast(DateOnly date)
        {
            return new OutboundWeatherForecast(date, 25, "Sunny");
        }
    }
}