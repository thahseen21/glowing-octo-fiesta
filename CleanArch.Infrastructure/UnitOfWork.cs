using CleanArch.Application.Common.Contracts.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        IDbConnection connection = null!;
        IDbTransaction transaction = null!;
        private readonly IHttpContextAccessor httpContextAccessor;

        #region PrivateRepositories
        private IWeatherForecastRepository weatherForecastRepository { get; set; } = null!;
        #endregion

        public UnitOfWork(IDbConnection dbConnection, IHttpContextAccessor httpContextAccessor)
        {
            this.connection = dbConnection;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IWeatherForecastRepository WeatherForecastRepository
        {
            get
            {
                if (this.weatherForecastRepository == null)
                {
                    this.weatherForecastRepository = (IWeatherForecastRepository)httpContextAccessor.HttpContext!.RequestServices.GetService(typeof(IWeatherForecastRepository)); ;
                }
                return weatherForecastRepository;
            }
        }

        public IDbConnection Connection
        {
            get { return connection; }
        }

        public IDbTransaction Transaction
        {
            get { return transaction; }
        }

        public IDbTransaction Begin()
        {
            transaction = connection.BeginTransaction();
            return transaction;
        }

        public void Commit()
        {
            transaction.Commit();
            Dispose();
        }

        public void Dispose()
        {
            if (transaction != null)
                transaction.Dispose();
            transaction = null;
        }

        public void Rollback()
        {
            transaction.Rollback();
            Dispose();
        }
    }
}
