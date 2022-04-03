using CleanArch.Application.Common.Utils;
using FluentValidation;

namespace CleanArch.Application.Feature.WeatherForecast.Commands.UpsertWeatherForecast
{
    public class UpsertWeatherForecastValidator : AbstractValidator<UpsertWeatherForecastCommand>
    {
        public UpsertWeatherForecastValidator(IErrorLocalizer localizer)
        {
            RuleFor(v => v.Summary)
                .NotEmpty().WithMessage(localizer.SummaryIsEmpty)
                .NotNull().WithMessage(localizer.SummaryIsNull);
        }
    }
}
