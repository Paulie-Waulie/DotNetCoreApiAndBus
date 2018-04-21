namespace DotNetCoreApi.Exceptions
{
    using System;

    public class DatabaseUnavailableException : Exception
    {
        public DatabaseUnavailableException(string message)
            : base(message)
        {
        }

        public DatabaseUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}