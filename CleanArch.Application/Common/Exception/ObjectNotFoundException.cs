namespace CleanArch.Application.Common.Exceptions
{
    // <summary>
    /// Thrown when an object that should exist but do not exists
    /// </summary>
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException()
        {
        }

        public ObjectNotFoundException(string message) : base(message)
        {
        }

        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
