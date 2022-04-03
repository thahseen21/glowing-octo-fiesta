using MediatR;

namespace CleanArch.Application.Feature.WeatherForecast.Commands.UpsertWeatherForecast
{
    public class UpsertWeatherForecastCommand : IRequest<WeatherForecastVM>
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }
    }
}
