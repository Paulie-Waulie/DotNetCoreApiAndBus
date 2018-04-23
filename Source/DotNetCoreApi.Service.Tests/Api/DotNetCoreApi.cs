namespace DotNetCoreApi.Service.Tests.Api
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using DotNetCore.Contracts.Rest;
    using Refit;

    public interface IDotNetCoreApi
    {
        [Put("/api/payments/{paymentReference}")]
        Task<HttpResponseMessage> CreatePayment(string paymentReference, Payment payment);

        [Get("/api/payments/{paymentReference}")]
        Task<HttpResponseMessage> GetPayment(string paymentReference);
    }
}
