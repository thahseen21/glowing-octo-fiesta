using CleanArch.Application.Common.Mappings;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Feature.WeatherForecast.Models
{
    public class AddWeatherForecastVM : WeatherForecastVM, IMapTo<WeatherForecastTbl>
    {
    }
}
