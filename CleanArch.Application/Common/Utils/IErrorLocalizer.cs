namespace CleanArch.Application.Common.Utils
{
    public interface IErrorLocalizer
    {
        public string ErrorInProcessing { get; }
        public string SummaryIsEmpty { get; }
        public string SummaryIsNull { get; }
    }
}
