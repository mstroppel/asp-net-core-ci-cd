using AspNetCoreSample.Outbound;

namespace AspNetCoreSample.Logic;

public class WeatherForecastService(IForecastStorage forecastStorage) : IWeatherForecastService
{
    private readonly IForecastStorage _forecastStorage = forecastStorage ?? throw new ArgumentNullException(nameof(forecastStorage));

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        var forecast = _forecastStorage.GetForecast(DateOnly.FromDateTime(DateTime.Now));
        var forecastTomorrow = _forecastStorage.GetForecast(DateOnly.FromDateTime(DateTime.Now).AddDays(1));
        return
        [
            new WeatherForecast(forecast.Date, forecast.TemperatureC, forecast.Summary),
            new WeatherForecast(forecastTomorrow.Date, forecastTomorrow.TemperatureC, forecastTomorrow.Summary),
        ];
    }
}
