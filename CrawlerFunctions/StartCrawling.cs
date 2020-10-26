using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CrawlerFunctions.Crawler;

namespace CrawlerFunctions
{
    public class StartCrawling
    {
        [FunctionName("StartCrawling")]
        // Cron job runs once per day at 1am UTC - 0 0 1 * * *
        public async Task Run([TimerTrigger("0 0 0 * * *"
#if DEBUG
            , RunOnStartup = true
#endif            
            )]TimerInfo myTimer,
            [Queue("Crawl-Site"), StorageAccount("AzureWebJobsStorage")] ICollector<SiteConfiguration> crawlSiteQueue,
            ILogger log)
        {
            log.LogInformation($"Function to start crawling initiated at: {DateTime.Now}");

            // TODO:  Check in the data store and then queue up each of the websites including the configuration for the site
            crawlSiteQueue.Add(new SiteConfiguration(0, "http://foodpantries.org", "FoodPantry"));
        }
    }
}
