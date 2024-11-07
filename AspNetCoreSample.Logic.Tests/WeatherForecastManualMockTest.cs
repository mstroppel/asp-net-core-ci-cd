using AspNetCoreSample.Outbound;

namespace AspNetCoreSample.Logic.Tests;

public class WeatherForecastManualMockTest
{
    [Fact]
    public void GetForecast_ShouldReturnFiveForecasts()
    {
        var systemUnderTest = new WeatherForecastService(new ForecastStorage());
        
        var forecasts = systemUnderTest.GetForecasts();
        
        Assert.NotNull(forecasts);
        Assert.Equal(6, forecasts.Count());
    }

    [Fact]
    public void GetForecasts_ShouldReturnCorrectTemperature()
    {
        var systemUnderTest = new WeatherForecastService(new ForecastStorage());
        
        var forecasts = systemUnderTest.GetForecasts();
        
        Assert.NotNull(forecasts);
        Assert.Equal(20, forecasts.First().TemperatureC);
    }
    private class ForecastStorage : IForecastStorage
    {
        public OutboundWeatherForecast GetForecast(DateOnly date)
        {
            return new OutboundWeatherForecast(date, 20, "Sunny");
        }
    }
}