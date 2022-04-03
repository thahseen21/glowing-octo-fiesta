namespace CleanArch.Application.Common.Exceptions
{
    public class ApplicationSqlException : Exception
    {
        public int ErrorCode { get; set; }

        public ApplicationSqlException()
        {
        }

        public ApplicationSqlException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApplicationSqlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
