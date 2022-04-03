using CleanArch.Application.Common.Model;
using MediatR;

namespace CleanArch.Application.Feature.WeatherForecast.Queries.GetWeatherForecast
{
    public class GetWeatherForecastQuery : IRequest<PaginatedResultVm<WeatherForecastVM>>
    {
    }
}
