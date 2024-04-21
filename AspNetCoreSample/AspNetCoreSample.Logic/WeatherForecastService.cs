using AspNetCoreSample.Outbound;

namespace AspNetCoreSample.Logic;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IForecastStorage _forecastStorage;

    public WeatherForecastService(IForecastStorage forecastStorage)
    {
        _forecastStorage = forecastStorage ?? throw new ArgumentNullException(nameof(forecastStorage));
    }

    public async IAsyncEnumerable<WeatherForecast> GetForecast()
    {
        var daysList = Enumerable.Range(1, 5);
        foreach (var index in daysList)
        {
            var forecast = await GetFromStorage(index);
            yield return forecast;
        }
    }
    
    private async Task<WeatherForecast> GetFromStorage(int daysFromNow)
    {
        var outbound = await _forecastStorage.GetForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(daysFromNow)));
        return new WeatherForecast(outbound.Date, outbound.TemperatureC, outbound.Summary);
    }
}
