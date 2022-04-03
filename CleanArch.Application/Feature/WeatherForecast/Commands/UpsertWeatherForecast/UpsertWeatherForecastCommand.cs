using CleanArch.Application.Feature.WeatherForecast.Models;
using MediatR;

namespace CleanArch.Application.Feature.WeatherForecast.Commands.UpsertWeatherForecast
{
    public class UpsertWeatherForecastCommand : AddWeatherForecastVM, IRequest<WeatherForecastVM>
    {
    }
}
