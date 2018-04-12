namespace DotNetCoreApi.Data
{
    using System.Threading.Tasks;
    using Contracts;
    using global::Contracts.Models;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;

    public interface ISavePaymentCommand
    {
        Task Save(PaymentReference paymentReference, Payment payment);
    }

    public class SavePaymentCommand : ISavePaymentCommand
    {
        private readonly IDocumentClient documentClient;

        public SavePaymentCommand(IDocumentClient documentClient)
        {
            this.documentClient = documentClient;
        }

        public Task Save(PaymentReference paymentReference, Payment payment)
        {
            var documentUri = UriFactory.CreateDocumentCollectionUri(DocumentDbSettings.DatabaseId, DocumentDbSettings.CollectionId);
            return this.documentClient.CreateDocumentAsync(documentUri, payment);
        }
    }
}
