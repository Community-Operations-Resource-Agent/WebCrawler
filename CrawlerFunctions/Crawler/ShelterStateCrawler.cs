namespace CrawlerFunctions.Crawler
{
    using System.Collections.Generic;
    using CrawlerFunctions.Models;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class ShelterStateCrawler
    {

        public static IList<ListingInformation> CrawlShelterStateWebSite(ListingInformation state, ChromeDriver driver)
        {
            IList<ListingInformation> cities = new List<ListingInformation>();
            driver.Navigate().GoToUrl(state.Url);

            IWebElement tableElement = driver.FindElement(By.TagName("table"));
            IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
            foreach (IWebElement row in tableRow)
            {
                IList<IWebElement> rowTD = row.FindElements(By.XPath("//td/a"));
                foreach (IWebElement td in rowTD)
                {
                    ListingInformation city = new ListingInformation
                    {
                        Name = td.Text,
                        Url = td.GetAttribute("href")
                    };
                    cities.Add(city);
                }

            }
            return cities;
        }
    }
}
