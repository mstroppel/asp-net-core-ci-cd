namespace AspNetCoreSample.Logic;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> GetForecast();
}