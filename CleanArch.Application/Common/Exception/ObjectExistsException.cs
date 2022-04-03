namespace CleanArch.Application.Common.Exceptions
{
    /// <summary>
    /// Thrown when an object that should not exist do exists
    /// </summary>
    public class ObjectExistsException : Exception
    {
        public ObjectExistsException()
        {
        }

        public ObjectExistsException(string message) : base(message)
        {
        }

        public ObjectExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
