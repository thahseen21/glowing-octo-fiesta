using CleanArch.Application.Common.Exceptions;
using CleanArch.Application.Common.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace CleanArch.Application.Common.Behaviours
{
    public class SqlExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Exception
    {
        private readonly ILogger<TRequest> logger;
        private readonly IErrorLocalizer _errorLocalizer;

        public SqlExceptionBehaviour(ILogger<TRequest> logger, IErrorLocalizer errorLocalizer)
        {
            this.logger = logger;
            this._errorLocalizer = errorLocalizer;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (SqlException ex)
            {
                var requestName = typeof(TRequest).Name;
                logger.LogError(ex, "SQL Exception for Request {Name} {@Request}", requestName, request);
                throw new ApplicationSqlException(_errorLocalizer.GetMessage(ex.Message), ex.Number);
            }
        }
    }
}
