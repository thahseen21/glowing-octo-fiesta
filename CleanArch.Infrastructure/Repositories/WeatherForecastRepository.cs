using CleanArch.Application.Common.Contracts.Repositories;
using CleanArch.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CleanArch.Infrastructure.Repositories
{
    public class WeatherForecastRepository : DapperRepositoryBase<WeatherForecastTbl>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(IDbConnection connection) : base(connection) { }

        readonly string GetWeatherForecastListStoreProcedure = "[dbo].[GetWeatherForecastList]";
        readonly string UpsertWeatherForecastStoreProcedure = "[dbo].[UpsertWeatherForecast]";


        public async Task<List<WeatherForecastTbl>> GetWeatherForecastList()
        {
            var weatherList = new List<WeatherForecastTbl>();
            using (var weatherListDbResult = await Connection.QueryMultipleAsync(GetWeatherForecastListStoreProcedure, null, null, null, CommandType.StoredProcedure).ConfigureAwait(false))
            {
                weatherList = weatherListDbResult.Read<WeatherForecastTbl>().ToList();
            }
            return weatherList;
        }

        public async Task<bool> UpsertWeatherForecast(WeatherForecastTbl weatherForecast)
        {
            await Connection.ExecuteAsync(UpsertWeatherForecastStoreProcedure, new
            {
                Summary = weatherForecast.Summary,
                TemperatureC = weatherForecast.TemperatureC,
                TemperatureF = weatherForecast.TemperatureF,
                Date = weatherForecast.Date
            }, null, null, CommandType.StoredProcedure).ConfigureAwait(false);

            return true;
        }
    }
}
