using CleanArch.Domain.Entities;

namespace CleanArch.Application.Common.Contracts.Repositories
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecastTbl>> GetWeatherForecastList();
        Task<bool> UpsertWeatherForecast(WeatherForecastTbl weatherForecast);
    }
}
