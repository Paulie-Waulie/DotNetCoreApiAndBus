namespace DotNetCoreApi.Exceptions
{
    using System;
    using System.Net;

    public class CosmosDatabaseUnavailableException : DatabaseUnavailableException
    {
        public CosmosDatabaseUnavailableException(HttpStatusCode? httpStatusCode, Exception innerException)
            : base ($"Cosmos operation failed with status {httpStatusCode}.", innerException)
        {
        }
    }
}