namespace AspNetCoreSample.Outbound;

public interface IForecastStorage
{
    Task<OutboundWeatherForecast> GetForecast(DateOnly date);
}