namespace DotNetCoreApi.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Exceptions;
    using Microsoft.ApplicationInsights;
    using Microsoft.AspNetCore.Http;

    public class ExceptionHandling
    {
        private readonly ExceptionTypeHttpStatusCodeMappings exceptionToStatusCodes = new ExceptionTypeHttpStatusCodeMappings();

        private readonly RequestDelegate next;
        private readonly TelemetryClient telemetryClient;

        public ExceptionHandling(RequestDelegate next, TelemetryClient telemetryClient)
        {
            this.next = next;
            this.telemetryClient = telemetryClient;
            this.exceptionToStatusCodes.AddMapping<DuplicatePaymentException>(HttpStatusCode.Conflict);
            this.exceptionToStatusCodes.AddMapping<DatabaseUnavailableException>(HttpStatusCode.ServiceUnavailable);
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                this.telemetryClient.TrackException(e);

                if (this.exceptionToStatusCodes.Map(e, out var statusCode))
                {
                    context.Response.StatusCode = (int)statusCode;
                    return;
                }

                throw;
            }
        }

        private class ExceptionTypeHttpStatusCodeMappings : Dictionary<Type, HttpStatusCode>
        {
            public void AddMapping<T>(HttpStatusCode httpStatusCode) where T : Exception
            {
                this.Add(typeof(T), httpStatusCode);
            }

            public bool Map(Exception exception, out HttpStatusCode httpStatusCode)
            {
                return this.FindByType(exception.GetType(), out httpStatusCode);
            }

            private bool FindByType(Type exceptionType, out HttpStatusCode httpStatusCode)
            {
                httpStatusCode = HttpStatusCode.InternalServerError;

                if (exceptionType == typeof(object))
                {
                    return false;
                }

                return this.TryGetValue(exceptionType, out httpStatusCode) || FindByType(exceptionType.BaseType, out httpStatusCode);
            }
        }
    }
}
