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
        public string SummaryIsEmpty => localizer["SummaryIsEmpty"];
        public string SummaryIsNull => localizer["SummaryIsNull"];


        // Checks whether key is present if not "ErrorInProcessing" is returned
        public string GetMessage(string key)
        {
            if (localizer[key] == key)
                return ErrorInProcessing;
            return localizer[key];
        }
    }
}
