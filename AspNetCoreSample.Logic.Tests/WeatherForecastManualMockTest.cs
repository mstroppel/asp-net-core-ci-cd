using AspNetCoreSample.Outbound;
using FluentAssertions;

namespace AspNetCoreSample.Logic.Tests;

public class WeatherForecastManualMockTest
{
    [Fact]
    public void GetForecast_ShouldReturnForecasts()
    {
        var sut = new WeatherForecastService(new ManualForecastStorageMock());

        var actual = sut.GetForecasts();

        actual.Should().HaveCount(5);
    }
    
    public class ManualForecastStorageMock : Outbound.IForecastStorage
    {
        public OutboundWeatherForecast GetForecast(DateOnly date)
        {
            return new (date, 25, "Sunny");
        }
    }
}