using System;
using System.Threading.Tasks;
using CrawlerFunctions.Common;
using CrawlerFunctions.Crawler;
using CrawlerFunctions.Providers;
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

        private readonly IServiceBusProvider _serviceBusProvider;

        private readonly IDatastoreProvider _datastoreProvider;

        public CrawlPage(ScrapingBrowser b, IServiceBusProvider serviceBusProvider, IDatastoreProvider datastoreProvider)
        {
            browser = b;
            this._serviceBusProvider = serviceBusProvider;
            this._datastoreProvider = datastoreProvider;
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
                //_serviceBusProvider.SendMessageAsync(CrawlerUtils.CreateMessage());
            }
            else
            {
                // If we're not at the last level, we're looking for names & links
                // Gather all the data and push into Cosmos DB
                //_datastoreProvider.InsertDocumentAsync(document);
            }
            

            // In the end, we push back on to the same queue if we have another 'level' in the crawler to go

        }
    }
}
