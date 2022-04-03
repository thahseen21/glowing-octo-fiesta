using CleanArch.Application.Common.Contracts.Repositories;
using CleanArch.Application.Common.Model;
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


        public async Task<PaginatedResultVm<WeatherForecastTbl>> GetWeatherForecastList()
        {
            var weatherList = new PaginatedResultVm<WeatherForecastTbl>();
            using (var weatherListDbResult = await Connection.QueryMultipleAsync(GetWeatherForecastListStoreProcedure, null, null, null, CommandType.StoredProcedure).ConfigureAwait(false))
            {
                weatherList.Items = weatherListDbResult.Read<WeatherForecastTbl>().ToList();
                weatherList.TotalCount = weatherList.Items.Count();
            }
            return weatherList;
        }

        public async Task<WeatherForecastTbl> UpsertWeatherForecast(WeatherForecastTbl weatherForecast)
        {
            await Connection.ExecuteAsync(UpsertWeatherForecastStoreProcedure, new
            {
                Summary = weatherForecast.Summary,
                TemperatureC = weatherForecast.TemperatureC,
                TemperatureF = weatherForecast.TemperatureF,
                Date = weatherForecast.Date
            }, null, null, CommandType.StoredProcedure).ConfigureAwait(false);

            return weatherForecast;
        }
    }
}
