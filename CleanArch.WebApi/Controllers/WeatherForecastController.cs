using CleanArch.Application.Feature.WeatherForecast;
using CleanArch.Application.Feature.WeatherForecast.Commands.UpsertWeatherForecast;
using CleanArch.Application.Feature.WeatherForecast.Queries.GetWeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IList<WeatherForecastVM>> GetWeatherForecast()
        {
            return await Mediator.Send(new GetWeatherForecastQuery());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpsertWeatherForecast(WeatherForecastVM weatherForecast)
        {
            UpsertWeatherForecastCommand command = new UpsertWeatherForecastCommand()
            {
                Date = DateTime.Now,
                TemperatureC = 10,
                Summary = weatherForecast.Summary
            };
            return Ok(await Mediator.Send(command));
        }
    }
}