namespace DotNetCoreApi.Data
{
    using System.Net;
    using System.Threading.Tasks;
    using Configuration;
    using DotNetCore.Contracts.Values;
    using Exceptions;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Options;
    using Model;
    using Newtonsoft.Json;

    public interface IGetPaymentQuery
    {
        Task<Payment> Execute(string paymentReference);
    }

    internal class GetPaymentQuery : IGetPaymentQuery
    {
        private readonly IDocumentClient documentClient;
        private readonly DocumentDbSettings dbSettings;

        public GetPaymentQuery(IDocumentClientFactory documentClientFactory, IOptions<DocumentDbSettings> dbSettings)
        {
            this.dbSettings = dbSettings.Value;
            this.documentClient = documentClientFactory.Create(this.dbSettings).Result;
        }

        public async Task<Payment> Execute(string paymentReference)
        {
            var documentUri = UriFactory.CreateDocumentUri(dbSettings.DatabaseId, dbSettings.CollectionId, paymentReference);

            try
            {
                var result = await this.documentClient.ReadDocumentAsync(documentUri);
                return JsonConvert.DeserializeObject<DocumentObject<Payment>>(result.Resource.ToString()).Unwrap();
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode.GetValueOrDefault().Equals(HttpStatusCode.NotFound))
                {
                    throw new PaymentNotFoundException(paymentReference);
                }

                e.ThrowUnavailableExceptionIfErrored();

                throw;
            }
        }
    }
}
