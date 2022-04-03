using CleanArch.Application.Common.Mappings;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Feature.WeatherForecast
{
    public class WeatherForecastVM : IMapFrom<WeatherForecastTbl>
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }
    }
}