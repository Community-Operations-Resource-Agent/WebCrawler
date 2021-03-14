using System;
using System.Text.RegularExpressions;
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

        //  private readonly IServiceBusProvider _serviceBusProvider;

        // private readonly IDatastoreProvider _datastoreProvider;

        public CrawlPage(ScrapingBrowser b)
        {
            browser = b;
            // this._serviceBusProvider = serviceBusProvider;
            // this._datastoreProvider = datastoreProvider;
        }

        [FunctionName("CrawlPage")]
        public async Task RunAsync(
            [QueueTrigger("Crawl-Site"), StorageAccount("AzureWebJobsStorage")] SiteConfiguration crawlConfig,
            [Queue("Crawl-Site"), StorageAccount("AzureWebJobsStorage")] ICollector<SiteConfiguration> crawlSiteQueue,
            ILogger log)
        {
            log.LogInformation($"Crawl Page started for URL {crawlConfig.PageUrl} at {DateTime.Now}");

            // Get the page
            var page = await browser.NavigateToPageAsync(new Uri(crawlConfig.PageUrl));

            // What Type? Current Level => look at the site config, determine type
            SiteSelector currentSelector = crawlConfig.SiteSelectors[crawlConfig.NextLevel];
            if (currentSelector.Type == "NameAndLinks")
            {
                // Get the selector and select out elements from the page
                var selectedItems = page.Html.SelectNodes(currentSelector.GroupSelector);
                foreach (var item in selectedItems)
                {
                    // Lift out name
                    // Lift out link
                    var parsedLink = item.Attributes["href"].Value; //lifted link

                    var parsedName = item.Attributes["title"].Value;


                    var nextConfig = crawlConfig.CreateNext(parsedName, parsedLink);
                    crawlSiteQueue.Add(nextConfig);

                    #if DEBUG
                        break;
                    #endif
                }
            }

            if (currentSelector.Type == "Details")
            {
                // Use selectors to get info from page
                // Display to the console
                var selectedItems = page.Html.SelectNodes(currentSelector.GroupSelector);
                foreach (var item in selectedItems)
                {
                    var name = "";
                    var url = "";
                    var location = "";
                    var hours = "";
                    var notes = "";
                }
            }
        }
    }
}