using AspNetCoreSample.Outbound;

namespace AspNetCoreSample.Logic;

public class WeatherForecastService(IForecastStorage forecastStorage) : IWeatherForecastService
{
    private readonly IForecastStorage _forecastStorage = forecastStorage ?? throw new ArgumentNullException(nameof(forecastStorage));

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        // TODO: implementation #1
        return [new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 16, "Sunny")];
    }
}
