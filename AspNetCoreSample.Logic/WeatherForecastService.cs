using AspNetCoreSample.Outbound;

namespace AspNetCoreSample.Logic;

public class WeatherForecastService(IForecastStorage forecastStorage) : IWeatherForecastService
{
    private readonly IForecastStorage _forecastStorage = forecastStorage ?? throw new ArgumentNullException(nameof(forecastStorage));

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        var forecasts = Enumerable.Range(0, 5).Select(x => _forecastStorage.GetForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(x))));
        return forecasts.Select(x => new WeatherForecast(x.Date, x.TemperatureC, x.Summary));
    }
}
