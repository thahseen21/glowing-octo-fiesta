using MediatR;

namespace CleanArch.Application.Feature.WeatherForecast.Queries.GetWeatherForecast
{
    public class GetWeatherForecastQuery : IRequest<List<WeatherForecastVM>>
    {
    }
}
