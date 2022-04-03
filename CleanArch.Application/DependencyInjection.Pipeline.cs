using CleanArch.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Application
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddPipeline(this IServiceCollection services)
        {
            #region Validation Behaviour
            // Generic pipeline behavior to check for any validation errors thrown by FluentValidation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SqlExceptionBehaviour<,>));

            // Logs any unhandled exceptions thrown within a request
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            #endregion

            return services;
        }
    }
}
