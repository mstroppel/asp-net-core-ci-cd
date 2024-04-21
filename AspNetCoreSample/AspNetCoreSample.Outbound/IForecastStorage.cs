namespace AspNetCoreSample.Outbound;

public interface IForecastStorage
{
    OutboundWeatherForecast GetForecast(DateOnly date);
}