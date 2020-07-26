using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CrawlerFunctions
{
    public static class StartCrawling
    {
        [FunctionName("Start Crawling")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Check CosmosDB source, to find all the sites we need to crawl
            // Then queue up for each of the crawlers for sites to run on the "sites-to-crawl" queue
        }
    }
}
