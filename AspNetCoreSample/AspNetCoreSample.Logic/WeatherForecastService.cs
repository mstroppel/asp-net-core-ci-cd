using AspNetCoreSample.Outbound;

namespace AspNetCoreSample.Logic;

public class WeatherForecastService(IForecastStorage forecastStorage) : IWeatherForecastService
{
    private readonly IForecastStorage _forecastStorage = forecastStorage ?? throw new ArgumentNullException(nameof(forecastStorage));

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        var forecasts = Enumerable.Range(1, 5).Select(index =>
        {
            var dateOfForecast = DateOnly.FromDateTime(DateTime.Now.AddDays(index));
            var outbound = _forecastStorage.GetForecast(dateOfForecast);
            return new WeatherForecast(outbound.Date, outbound.TemperatureC, outbound.Summary);
        });
        return forecasts;
    }
}
