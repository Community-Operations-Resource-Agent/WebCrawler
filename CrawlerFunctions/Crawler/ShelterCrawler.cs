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
        public static IList<ListingInformation> CrawlShelterWebSite(String url, ChromeDriver driver)
        {
            IList<ListingInformation> stateList = new List<ListingInformation>();
            driver.Navigate().GoToUrl(@url);

            IWebElement tableElement = driver.FindElement(By.TagName("table"));
            IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
            foreach (IWebElement row in tableRow)
            {
                IList<IWebElement> rowTD = row.FindElements(By.XPath("//td/a"));
                foreach (IWebElement td in rowTD)
                {
                    ListingInformation state = new ListingInformation
                    {
                        Name = td.Text,
                        Url = td.GetAttribute("href")
                    };
                    stateList.Add(state);
                }

            }
            return stateList;
        }
    }
}