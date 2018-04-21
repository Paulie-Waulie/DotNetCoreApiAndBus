namespace DotNetCoreApi.Exceptions
{
    using System.Net;
    using Microsoft.Azure.Documents;

    internal static class DocumentClientExceptionExtensions
    {
        internal static void ThrowUnavailableExceptionIfErrored(this DocumentClientException exception)
        {
            if (exception.StatusCode.GetValueOrDefault() >= HttpStatusCode.InternalServerError)
            {
                throw new CosmosDatabaseUnavailableException(exception.StatusCode, exception);
            }
        }
    }
}