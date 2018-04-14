namespace DotNetCoreApi.Data
{
    using System.Threading.Tasks;
    using Configuration;
    using Contracts;
    using global::Contracts.Models;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Options;

    public interface ISavePaymentCommand
    {
        Task Save(PaymentReference paymentReference, Payment payment);
    }

    internal class SavePaymentCommand : ISavePaymentCommand
    {
        private readonly IDocumentClient documentClientFactory;
        private readonly DocumentDbSettings dbSettings;

        public SavePaymentCommand(IDocumentClientFactory documentClientFactory, IOptions<DocumentDbSettings> dbSettings)
        {
            this.dbSettings = dbSettings.Value;
            this.documentClientFactory = documentClientFactory.Create(this.dbSettings).Result;
        }

        public Task Save(PaymentReference paymentReference, Payment payment)
        {
            var documentUri = UriFactory.CreateDocumentCollectionUri(this.dbSettings.DatabaseId, this.dbSettings.CollectionId);
            return this.documentClientFactory.CreateDocumentAsync(documentUri, payment);
        }
    }
}
