using CrawlerFunctions.Providers;
using Microsoft.Extensions.Logging;

namespace CrawlerFunctions.Crawler
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using CrawlerFunctions.Models;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    /// <summary>
    /// Crawls shelter websites
    /// </summary>
    public static class ShelterCrawler
    {
        public static void CrawlShelterWebSite(ChromeDriver driver, IDatastoreProvider dbProvider,  ILogger log)
        {
            foreach (var crawler in GetShelterCrawlers(driver, dbProvider, log))
            {
                crawler.Crawl();
            }
        }

        /// <summary>
        /// Returns a list of shelter crawler
        /// for different websites
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        private static List<IShelterCrawler> GetShelterCrawlers(ChromeDriver driver, IDatastoreProvider dbProvider, ILogger log)
        {
            List<IShelterCrawler> crawlers = new List<IShelterCrawler>
            {
                new HomelessShelterDirectorySiteCrawler(driver,  dbProvider, log)
            };
            return crawlers;
        }

    }
}