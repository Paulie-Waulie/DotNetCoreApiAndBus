namespace DotNetCoreApi.Configuration
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;

    internal interface IDocumentClientFactory
    {
        Task<IDocumentClient> Create(DocumentDbSettings dbSettings);
    }

    internal class DocumentClientFactory : IDocumentClientFactory
    {
        public async Task<IDocumentClient> Create(DocumentDbSettings dbSettings)
        {
            var client = new DocumentClient(dbSettings.EndpointUri, dbSettings.AuthKey);

            var databaseUri = UriFactory.CreateDatabaseUri(dbSettings.DatabaseId);

            await client.CreateDatabaseIfNotExistsAsync(new Database { Id = dbSettings.DatabaseId });
            await client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, new DocumentCollection { Id = dbSettings.CollectionId });
            
            return client;
        }
    }
}
