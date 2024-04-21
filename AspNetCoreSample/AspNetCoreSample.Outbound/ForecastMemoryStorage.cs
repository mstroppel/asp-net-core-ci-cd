namespace AspNetCoreSample.Outbound;

public class ForecastMemoryStorage : IForecastStorage
{
    private readonly string[] _summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
    ];

    public OutboundWeatherForecast GetForecast(DateOnly date)
    {
        return new OutboundWeatherForecast(
            date,
            Random.Shared.Next(-20, 55),
            _summaries[Random.Shared.Next(_summaries.Length)]);
    }
}