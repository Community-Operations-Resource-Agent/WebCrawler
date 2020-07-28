namespace CrawlerFunctions.Providers
{
    using System;

    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;

    /// <summary>
    /// Datastore provider
    /// </summary>
    public interface IDatastoreProvider
    {
        /// <summary>
        /// Inserts document in datastore
        /// </summary>
        /// <param name="documentCollectionUri"></param>
        /// <param name="document"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<Document> InsertDocumentAsync(Uri documentCollectionUri, object document, RequestOptions options);

        /// <summary>
        /// Upserts document in datastore
        /// </summary>
        /// <param name="documentUri"></param>
        /// <param name="document"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<Document> UpsertDocumentAsync(Uri documentUri, object document, RequestOptions options);

    }
}
