using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CrawlerFunctions.Common
{
    public static class CosmosDBUtils
    {
        private static string databaseId = "CrawlerData";

        public static Database CrawlerDB;
        public static CosmosClient CrawlerClient;

        private static async Task CreateCrawlerClientAsync(ILogger log)
        {
            try
            {
                // Create a new instance of the Cosmos Client
                CrawlerClient = new CosmosClient(AppSettings.EndpointUrl, AppSettings.PrimaryKey, new CosmosClientOptions()
                {
                    ConnectionMode = ConnectionMode.Gateway
                });
                log.LogInformation("CreateCrawlerClientAsync, successfully created CosmosClient");
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
            }
        }

        public static async Task CreateCrawlerDatabaseAsync(ILogger log)
        {
            if (CrawlerClient == null)
            {
                await CreateCrawlerClientAsync(log);
            }
            try
            {
                // Create a new database
                CrawlerDB = await CrawlerClient.CreateDatabaseIfNotExistsAsync(databaseId, 400);
                log.LogInformation("CreateCrawlerClientAsync, successfully created Database");
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
            }
        }

        public static async Task<Container> CreateCosmosContainerAsync(string containerId, string partitionkey, ILogger log)
        {
            if (CrawlerClient == null)
            {
                await CreateCrawlerClientAsync(log);
            }
            if (CrawlerDB == null)
            {
                await CreateCrawlerDatabaseAsync(log);
            }
            try
            {
                log.LogInformation("Creating Container " + containerId);
                return await CrawlerDB.CreateContainerIfNotExistsAsync(containerId, partitionkey);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
            }
            return null;
        }
        public static async Task AddItemToContainerAsync<T>(
            T item,
            Container container,
            string id,
            string partitiokey,
            ILogger log)
        {
            try
            {
                await container.CreateItemAsync<T>(item);
                log.LogInformation("Successfully added item with id: {0} \n", id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                log.LogInformation("Item in database with id: {0} already exists\n", id);
            }
            catch (CosmosException ex)
            {
                log.LogError("Failed to add item in database with id: {0} \n", id);
                log.LogError(ex.Message);

            }
        }
        public static async Task QueryContainerItemsAsync<T>(Container container, string query, ILogger log)
        {
            log.LogInformation("Running query: {0}\n", query);

            QueryDefinition queryDefinition = new QueryDefinition(query);
            FeedIterator<T> queryResultSetIterator = container.GetItemQueryIterator<T>(queryDefinition);

            List<T> entries = new List<T>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (T entry in currentResultSet)
                {
                    entries.Add(entry);
                    log.LogInformation("\tRead {0}\n", entry);
                }
            }
        }

        public static async Task ReplaceContainerItemAsync<T>(T item, Container container, string id, string partitionkey)
        {
            ItemResponse<T> readResponse = await container.ReadItemAsync<T>(id, new PartitionKey(partitionkey));

            // replace the item with the updated content
            readResponse = await container.ReplaceItemAsync<T>(item, id, new PartitionKey(partitionkey));
            Console.WriteLine("Updated Family [{0},{1}].\n \tBody is now: {2}\n", id, partitionkey, readResponse);
        }
        public static async Task DeleteCaontainerItemAsync<T>(T item, Container container, string id, string partitionkey, ILogger log)
        {

            // Delete an item. Note we must provide the partition key value and id of the item to delete
            ItemResponse<T> wakefieldFamilyResponse = await container.DeleteItemAsync<T>(id, new PartitionKey(partitionkey));
            log.LogInformation("Deleted Family [{0},{1}]\n", partitionkey, id);
        }

        public static async Task DeleteDatabaseAndCleanupAsync(Database database, ILogger log)
        {
            if (database != null)
            {
                DatabaseResponse databaseResourceResponse = await database.DeleteAsync();
                // Also valid: await this.cosmosClient.Databases["FamilyDatabase"].DeleteAsync();

                log.LogInformation("Deleted Database: {0}\n", databaseId);

                if (CrawlerClient != null)
                {
                    //Dispose of CosmosClient
                    CrawlerClient.Dispose();
                }
            }
        }
    }
}
