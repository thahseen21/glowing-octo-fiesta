namespace CleanArch.Application.Common.Utils
{
    public interface IErrorLocalizer
    {
        public string ErrorInProcessing { get; }
        public string SummaryIsEmpty { get; }
        public string SummaryIsNull { get; }
        public string DateIsAhead { get; }
        public string DateIsNull { get; }
        public string TemperatureIsNull { get; }

    }
}
