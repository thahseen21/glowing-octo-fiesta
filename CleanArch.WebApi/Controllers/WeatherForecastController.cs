using CleanArch.Application.Common.Model;
using CleanArch.Application.Feature.WeatherForecast;
using CleanArch.Application.Feature.WeatherForecast.Commands.UpsertWeatherForecast;
using CleanArch.Application.Feature.WeatherForecast.Queries.GetWeatherForecast;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ApiControllerBase
    {
        [HttpGet("[action]")]
        public async Task<PaginatedResultVm<WeatherForecastVM>> GetWeatherForecast()
        {
            return await Mediator.Send(new GetWeatherForecastQuery());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpsertWeatherForecast(WeatherForecastVM weatherForecast)
        {
            UpsertWeatherForecastCommand command = new UpsertWeatherForecastCommand()
            {
                Date = weatherForecast.Date,
                TemperatureC = weatherForecast.TemperatureC,
                Summary = weatherForecast.Summary
            };
            return Ok(await Mediator.Send(command));
        }
    }
}