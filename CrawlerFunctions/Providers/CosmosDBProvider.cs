namespace CrawlerFunctions.Providers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;

    public class CosmosDBProvider : IDatastoreProvider
    {
        private readonly IDocumentClient cosmosDbClient;
        private readonly Uri _documentCollectionUri;
        private RequestOptions _options;


        public CosmosDBProvider(IDocumentClient cosmosDbClient, string databaseName, string collectionName)
        {
            if (cosmosDbClient == null)
            {
                throw new ArgumentNullException(nameof(cosmosDbClient));
            }
            if (databaseName == null)
            {
                throw new ArgumentNullException(nameof(databaseName));
            }
            if (collectionName == null)
            {
                throw new ArgumentNullException(nameof(collectionName));
            }
            this.cosmosDbClient = cosmosDbClient;
            this._documentCollectionUri = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);
        }

        /// <summary>
        /// Insert document in database
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<Document> InsertDocumentAsync(object document)
        {
            ResourceResponse<Document> response = await this.cosmosDbClient.CreateDocumentAsync(_documentCollectionUri, document, _options, true);

            return ((response != null) && (response.Resource != null)) ?  response.Resource : null;
        
        }

        /// <summary>
        /// Upserts document in database
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<Document> UpsertDocumentAsync(object document)
        {
            ResourceResponse<Document> response = await this.cosmosDbClient.UpsertDocumentAsync(_documentCollectionUri, document, _options);

            return ((response != null) && (response.Resource != null)) ? response.Resource : null;
        }
    }
}
