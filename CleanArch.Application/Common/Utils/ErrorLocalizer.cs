using CleanArch.Application.Resources;
using Microsoft.Extensions.Localization;

namespace CleanArch.Application.Common.Utils
{
    public class ErrorLocalizer : IErrorLocalizer
    {
        private readonly IStringLocalizer<Resource> localizer;

        public ErrorLocalizer(IStringLocalizer<Resource> localizer)
        {
            this.localizer = localizer;
        }

        public string ErrorInProcessing => localizer["ErrorInProcessing"];
        public string UnauthorizedAccess => localizer["UnauthorizedAccess"];
        public string NotImplemented => localizer["NotImplemented"];


        #region Feature WeatherForecastError
        public string InvalidWeatherForecastCommand => localizer["InvalidWeatherForecastCommand"];
        public string SummaryIsEmpty => localizer["SummaryIsEmpty"];
        public string SummaryIsNull => localizer["SummaryIsNull"];
        public string DateIsAhead => localizer["DateIsAhead"];
        public string DateIsNull => localizer["DateIsNull"];
        public string TemperatureIsNull => localizer["TemperatureIsNull"];
        #endregion



        // Checks whether key is present if not "ErrorInProcessing" is returned for undefined errors
        public string GetMessage(string key)
        {
            var value = localizer[key];
            if (value == key)
                return ErrorInProcessing;
            return value;
        }
    }
}
