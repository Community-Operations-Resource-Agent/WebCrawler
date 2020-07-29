using System;
using CrawlerFunctions.Common;
using CrawlerFunctions.Crawler;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CrawlerFunctions
{
    public static class StartCrawling
    {
        [FunctionName("StartCrawling")]
        public static async Task Run([TimerTrigger("0 0 * * * *"
#if DEBUG
            , RunOnStartup = true
#endif            
            )]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var currentDir = Environment.CurrentDirectory;
            System.Environment.SetEnvironmentVariable("PATH", currentDir, EnvironmentVariableTarget.Process);

            AppSettings.LoadAppSettings(currentDir);
            // Create Cosmos Database
            await CosmosDBUtils.CreateCrawlerDatabaseAsync(log);

            // Create the Chrome driver
            var options = new ChromeOptions()
            {
                AcceptInsecureCertificates = true,
                PageLoadStrategy = PageLoadStrategy.Normal
            };


            options.AddArgument("headless");
            var driver = new ChromeDriver(options);

            // Kick off the food pantry crawling
            var data = FoodPantrySiteCrawler.CrawlFoodPantryWebSiteAsync(driver, log);

            // Kick off the Shelter crawling
            var data2 = ShelterCrawler.CrawlShelterWebSite("", driver);

            return;
        }
    }
}
