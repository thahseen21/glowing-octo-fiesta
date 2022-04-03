namespace CleanArch.Application.Common.Utils
{
    public interface IErrorLocalizer
    {
        string GetMessage(string key);
        public string ErrorInProcessing { get; }
        public string UnauthorizedAccess { get; }
        public string NotImplemented { get; }


        #region Feature WeatherForecastError
        public string InvalidWeatherForecastCommand { get; }
        public string SummaryIsEmpty { get; }
        public string SummaryIsNull { get; }
        public string DateIsAhead { get; }
        public string DateIsNull { get; }
        public string TemperatureIsNull { get; }
        #endregion
    }
}
