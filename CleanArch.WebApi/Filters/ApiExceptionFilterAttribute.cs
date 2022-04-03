using CleanArch.Application.Common.Exceptions;
using CleanArch.WebApi.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArch.WebApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ApiModelValidationException), HandleApiModelValidationException },
                { typeof(ObjectNotFoundException), HandleNotFoundException },
                { typeof(ObjectExistsException), HandleExistsException},
                { typeof(DataValidationException), HandleValidationException },
                { typeof(ApplicationSqlException), HandleSqlException },
            };
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as DataValidationException;
            var details = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Data validation error",
                Detail = exception!.Message
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleApiModelValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ApiModelValidationException;
            var errorMessages = exception!.Errors.Select(e => string.Join(Environment.NewLine, e.Value)).ToArray();

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = string.Join(Environment.NewLine, errorMessages)
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (exceptionHandlers.ContainsKey(type))
            {
                exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Detail = "An error occurred while processing your request."
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as ObjectNotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.3.5",
                Title = "The specified resource was not found.",
                Detail = exception!.Message
            };

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleExistsException(ExceptionContext context)
        {
            var exception = context.Exception as ObjectExistsException;

            var details = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "The specified resource already exists",
                Detail = exception!.Message
            };

            context.Result = new ConflictObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleSqlException(ExceptionContext context)
        {
            var exception = context.Exception as ApplicationSqlException;

            var details = new ExceptionDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = exception!.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Detail = exception.Message,
                ErrorCode = exception.ErrorCode
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;

        }
    }
}
