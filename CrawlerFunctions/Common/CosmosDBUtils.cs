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
        private static readonly string EndpointUri = @"https://crawlerdata.documents.azure.com:443/";
        private static readonly string PrimaryKey = @"wzqHc8ZcxGMPZLQmxjoz3oKVYJo08rEPOfcsjgYj4yHS0jj9koQtJlk5ZR9Q089yGMrGndHEEefPyrkwnBf79w==";

        private static string databaseId = "CrawlerData";

        public static Database CrawlerDB;
        public static CosmosClient CrawlerClient;

        private static async Task CreateCrawlerClientAsync(ILogger log)
        {
            try
            {
                // Create a new instance of the Cosmos Client
                CrawlerClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions()
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

        public static async Task<Container> CreateCosmosContainerAsync(string containerId, ILogger log)
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
                return await CrawlerDB.CreateContainerIfNotExistsAsync(containerId, "/partitiokey");
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
            }
            return null;
        }
        public static async Task AddItemAsync<T>(
            T item,
            Container container,
            string id,
            string partitiokey,
            ILogger log)
        {
            try
            {
                // Read the item to see if it exists
                ItemResponse<T> addEntryResponse = await container.ReadItemAsync<T>(id, new PartitionKey(partitiokey));
                log.LogInformation("Item in database with id: {0} already exists\n", id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                ItemResponse<T> addEntryResponse = await container.CreateItemAsync<T>(item, new PartitionKey(partitiokey));
            }
        }
    }
}
