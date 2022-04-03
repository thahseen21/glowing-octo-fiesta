namespace CleanArch.Application.Common.Exceptions
{
    public class DataValidationException : Exception
    {
        public DataValidationException(string msg) : base(msg)
        { }
    }
}
