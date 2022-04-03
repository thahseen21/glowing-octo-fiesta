using CleanArch.Domain.Common.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace CleanArch.Infrastructure
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IAppSettings appSettings)
        {
            #region DBConnection
            services.AddScoped<IDbConnection>(db => new SqlConnection(
                    appSettings.ConnectionStrings.DefaultConnection));
            #endregion

            #region Repositories
            services.AddRepositories();
            #endregion

            return services;
        }
    }
}
