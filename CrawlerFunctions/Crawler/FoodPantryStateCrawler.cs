using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CrawlerFunctions
{
    public static class FoodPantryStateCrawler
    {
        public static async Task CrawlFoodPantrySateSiteAsync(FoodPantryState state, ChromeDriver driver, ILogger log)
        {
            log.LogInformation("Start to crawl {0} web page", state.Name);

            IList<FoodPantryCity> cityList = new List<FoodPantryCity>();

            driver.Navigate().GoToUrl(state.Url);

            IWebElement tableElement = driver.FindElement(By.TagName("table"));
            IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
            IList<IWebElement> rowTD;
            foreach (IWebElement row in tableRow)
            {
                rowTD = row.FindElements(By.XPath("//td/a"));
                foreach (IWebElement td in rowTD)
                {
                    FoodPantryCity city = new FoodPantryCity();
                    city.CityName = td.Text;
                    city.Url = td.GetAttribute("href");
                    city.State = state;
                    cityList.Add(city);
                    log.LogInformation("successfully find city {0} in state {1}", city.CityName, state.Name);
                    await FoodPantryCityCrawler.CrawlFoodPantryCityWebSite(city, driver, log);
                }

            }
            //state.Cities = cityList;
            driver.Navigate().Back();
        }
    }
}
