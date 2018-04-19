namespace DotNetCoreApi.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Exceptions;
    using Microsoft.AspNetCore.Http;

    public class ExceptionHandling
    {
        private readonly ExceptionTypeHttpStatusCodeMappings exceptionToStatusCodes = new ExceptionTypeHttpStatusCodeMappings();

        private readonly RequestDelegate next;

        public ExceptionHandling(RequestDelegate next)
        {
            this.next = next;
            this.exceptionToStatusCodes.AddMapping<DuplicatePaymentException>(HttpStatusCode.Conflict);
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
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
                return this.TryGetValue(exception.GetType(), out httpStatusCode);
            }
        }
    }
}
