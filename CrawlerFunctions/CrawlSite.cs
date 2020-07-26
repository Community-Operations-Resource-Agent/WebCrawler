using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CrawlerFunctions
{
    public static class CrawlSite
    {
        [FunctionName("CrawlSite")]
        public static void Run([QueueTrigger("sites-to-crawl", Connection = "")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
