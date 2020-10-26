using System;
using System.Threading.Tasks;
using CrawlerFunctions.Crawler;
using HtmlAgilityPack;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace CrawlerFunctions
{
    public class CrawlPage
    {
        private readonly ScrapingBrowser browser;

        public CrawlPage(ScrapingBrowser b)
        {
            browser = b;
        }

        [FunctionName("CrawlPage")]
        public async Task RunAsync(
            [QueueTrigger("Crawl-Site"), StorageAccount("AzureWebJobsStorage")] SiteConfiguration crawlConfig,
            [Queue("Crawl-Site"), StorageAccount("AzureWebJobsStorage")] ICollector<SiteConfiguration> crawlSiteQueue,
            ILogger log)
        {
            log.LogInformation($"Crawl Page started for URL {crawlConfig.URL} at {DateTime.Now}");

            // Get the page
            var page = await browser.NavigateToPageAsync(new Uri(crawlConfig.URL));

            // Are we looking for links or details?  If we're at the last level, we are looking for details
            if (crawlConfig.Level >= crawlConfig.Selectors.Length - 1)
            {
                // Links:  Parse the URLs, Names and push back on to the queue
            }
            else
            {
                // If we're not at the last level, we're looking for names & links
                // Gather all the data and push into Cosmos DB

            }
            

            // In the end, we push back on to the same queue if we have another 'level' in the crawler to go

        }
    }
}
