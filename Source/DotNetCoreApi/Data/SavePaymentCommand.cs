namespace DotNetCoreApi.Data
{
    using System.Net;
    using System.Threading.Tasks;
    using Configuration;
    using Exceptions;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Options;
    using Model;

    public interface ISavePaymentCommand
    {
        Task Save(Payment payment);
    }

    internal class SavePaymentCommand : ISavePaymentCommand
    {
        private readonly IDocumentClient documentClient;
        private readonly DocumentDbSettings dbSettings;

        public SavePaymentCommand(IDocumentClientFactory documentClientFactory, IOptions<DocumentDbSettings> dbSettings)
        {
            this.dbSettings = dbSettings.Value;
            this.documentClient = documentClientFactory.Create(this.dbSettings).Result;
        }

        public async Task Save(Payment payment)
        {
            var documentUri = UriFactory.CreateDocumentCollectionUri(this.dbSettings.DatabaseId, this.dbSettings.CollectionId);

            try
            {
                await this.documentClient.CreateDocumentAsync(documentUri, payment.Wrap(() => payment.PaymentReference.Value));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode.GetValueOrDefault().Equals(HttpStatusCode.Conflict))
                {
                    throw new DuplicatePaymentException(payment.PaymentReference);
                }

                e.ThrowUnavailableExceptionIfErrored();

                throw;
            }
        }
    }
}
