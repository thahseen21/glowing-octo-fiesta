using CleanArch.Application.Common.Mappings;
using CleanArch.Application.Common.Utils;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArch.Application
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region AutoMapper
            var mappingConfig = new AutoMapperConfig().Configure();
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region FluentValidation
            services.AddFluentValidation(fv => fv.DisableDataAnnotationsValidation = true);
            #endregion

            #region Localisation
            services.AddLocalization();
            services.AddSingleton(typeof(IErrorLocalizer), typeof(ErrorLocalizer));
            #endregion

            #region Pipeline 
            services.AddPipeline();
            #endregion

            return services;
        }

    }
}
