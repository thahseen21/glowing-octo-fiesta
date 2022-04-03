using CleanArch.Application.Common.Utils;
using CleanArch.WebApi.Common.Model;
using System.Data;
using System.Net;

namespace CleanArch.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IErrorLocalizer _errorLocalizer;
        private readonly ILogger logger;
        public ErrorHandlingMiddleware(RequestDelegate next, IErrorLocalizer errorLocalizer, ILogger<ErrorHandlingMiddleware> loggerFactory)
        {
            this.next = next;
            this._errorLocalizer = errorLocalizer;
            this.logger = loggerFactory;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpResponse response = context.Response;
            response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            logger.LogError(exception, exception.Message);
            response.ContentType = "application/json";
            string message = _errorLocalizer.ErrorInProcessing;
            switch (exception)
            {
                case FluentValidation.ValidationException fv:
                    message = string.Join(",", fv.Errors.Select(e => $"{e.ErrorMessage } {Environment.NewLine}"));
                    response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                    break;
                case UnauthorizedAccessException ua:
                    message = _errorLocalizer.UnauthorizedAccess;
                    response.Headers.Remove("TokenExpired");
                    response.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
                    break;
                case Microsoft.Data.SqlClient.SqlException sqlEx:
                    message = _errorLocalizer.GetMessage(exception.Message);
                    response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                    break;
                case NotImplementedException _:
                    message = _errorLocalizer.NotImplemented;
                    response.StatusCode = HttpStatusCode.NotImplemented.GetHashCode();
                    break;
            }

            var details = new ExceptionDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.3.5",
                Title = "The specified resource was not found.",
                Detail = message,
                Status = response.StatusCode
            };

            return response.WriteAsync(details.ToString());
        }
    }
}
