namespace DotNetCoreApi.Configuration
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Configuration;

    internal static class DocumentClientFactory
    {
        internal static async Task<IDocumentClient> Create(IConfiguration configuration)
        {
            var endpoint = new Uri(configuration["DocumentDB:EndpointUri"]);
            var authKey = configuration["DocumentDB:AuthKey"];
            var client = new DocumentClient(endpoint, authKey);

            Uri databaseUri = UriFactory.CreateDatabaseUri(DocumentDbSettings.DatabaseId);

            await client.CreateDatabaseIfNotExistsAsync(new Database { Id = DocumentDbSettings.DatabaseId });
            await client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, new DocumentCollection() { Id = DocumentDbSettings.CollectionId });
            
            return client;
        }

        private static async Task CreateDatabaseIfNotExists(IDocumentClient client)
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DocumentDbSettings.DatabaseId)).ConfigureAwait(false);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DocumentDbSettings.DatabaseId }).ConfigureAwait(false);
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task<DocumentCollection> CreateCollectionIfNotExists(IDocumentClient client)
        {
            Uri documentCollectionUri = UriFactory.CreateDocumentCollectionUri(DocumentDbSettings.DatabaseId, DocumentDbSettings.CollectionId);

            try
            {
                return await client.ReadDocumentCollectionAsync(documentCollectionUri).ConfigureAwait(false);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return
                        await client.CreateDocumentCollectionAsync(
                            UriFactory.CreateDatabaseUri(DocumentDbSettings.DatabaseId),
                            new DocumentCollection
                            {
                                Id = DocumentDbSettings.CollectionId,
                            });
                }

                throw;
            }
        }
    }
}
