using CleanArch.Application.Common.Model;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Common.Contracts.Repositories
{
    public interface IWeatherForecastRepository
    {
        Task<PaginatedResultVm<WeatherForecastTbl>> GetWeatherForecastList();
        Task<WeatherForecastTbl> UpsertWeatherForecast(WeatherForecastTbl weatherForecast);
    }
}
