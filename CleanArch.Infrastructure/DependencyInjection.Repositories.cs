using CleanArch.Application.Common.Contracts.Repositories;
using CleanArch.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infrastructure
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            #region Repositories
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            return services;
        }
    }
}
