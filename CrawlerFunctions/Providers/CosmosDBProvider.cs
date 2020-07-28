using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace CrawlerFunctions.Providers
{
    public class CosmosDBProvider : IDatastoreProvider
    {
        private readonly IDocumentClient cosmosDbClient;

        public CosmosDBProvider(IDocumentClient cosmosDbClient)
        {
            if (cosmosDbClient == null)
            {
                throw new ArgumentNullException(nameof(cosmosDbClient));
            }
            this.cosmosDbClient = cosmosDbClient;
        }

        /// <summary>
        /// Insert document in database
        /// </summary>
        /// <param name="documentCollectionUri"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<Document> InsertDocumentAsync(Uri documentCollectionUri, object document, RequestOptions options)
        {
            ResourceResponse<Document> response = await this.cosmosDbClient.CreateDocumentAsync(documentCollectionUri, document, options, true);

            return ((response != null) && (response.Resource != null)) ?  response.Resource : null;
        
        }

        /// <summary>
        /// Upserts document in database
        /// </summary>
        /// <param name="documentUri"></param>
        /// <param name="document"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<Document> UpsertDocumentAsync(Uri documentUri, object document, RequestOptions options)
        {
            ResourceResponse<Document> response = await this.cosmosDbClient.UpsertDocumentAsync(documentUri, document, options);

            return ((response != null) && (response.Resource != null)) ? response.Resource : null;
        }
    }
}
