using AspNetCoreSample.Outbound;

namespace AspNetCoreSample.Logic;

public class WeatherForecastService(IForecastStorage forecastStorage) : IWeatherForecastService
{
    private readonly IForecastStorage _forecastStorage = forecastStorage ?? throw new ArgumentNullException(nameof(forecastStorage));

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        var weatherForecasts = Enumerable.Range(1, 5).Select(index =>
        {
            var dayOfForecast = DateOnly.FromDateTime(DateTime.Now.AddDays(index));
            var (dateOnly, temperatureC, summary) = _forecastStorage.GetForecast(dayOfForecast);
            return new WeatherForecast(dateOnly, temperatureC, summary);
        });
        return weatherForecasts;
    }
}
