namespace AspNetCoreSample.Logic;

public interface IWeatherForecastService
{
    IAsyncEnumerable<WeatherForecast> GetForecast();
}