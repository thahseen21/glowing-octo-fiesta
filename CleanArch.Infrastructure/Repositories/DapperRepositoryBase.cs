using CleanArch.Application.Common.Contracts.Repositories;
using System.Data;

namespace CleanArch.Infrastructure.Repositories
{
    public abstract class DapperRepositoryBase<TEntity> : IGenericRepository<TEntity>
       where TEntity : class
    {
        protected readonly IDbConnection Connection;
        public DapperRepositoryBase(IDbConnection connection)
        {
            this.Connection = connection;
        }
    }
}
