using System.Data;

namespace CleanArch.Application.Common.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        #region Repositories
        IWeatherForecastRepository WeatherForecastRepository { get; }
        #endregion

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IDbTransaction Begin();
        void Commit();
        void Rollback();
    }
}
