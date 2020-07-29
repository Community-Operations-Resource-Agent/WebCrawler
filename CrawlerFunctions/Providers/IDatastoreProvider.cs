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
        /// <param name="document"></param>
        /// <returns></returns>
        Task<Document> InsertDocumentAsync(object document);

        /// <summary>
        /// Upserts document in datastore
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task<Document> UpsertDocumentAsync(object document);

    }
}
