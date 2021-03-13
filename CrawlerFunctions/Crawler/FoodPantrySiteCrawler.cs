using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using OpenQA.Selenium;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CrawlerFunctions
{
    public static class FoodPantrySiteCrawler
    {
        private static string FoodPantrySiteURL = @"https://www.foodpantries.org/";

        public static async Task<IList<FoodPantryState>> CrawlFoodPantryWebSiteAsync(ChromeDriver driver, ILogger log)
        {
            log.LogInformation("Start to crawl FoodPantry web site page");

            IList<FoodPantryState> stateList = new List<FoodPantryState>();
           
            driver.Navigate().GoToUrl(FoodPantrySiteURL);

            IWebElement tableElement = driver.FindElement(By.TagName("table"));
            IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
            IList<IWebElement> rowTD;
            foreach (IWebElement row in tableRow)
            {
                rowTD = row.FindElements(By.XPath("//td/a"));
                foreach (IWebElement td in rowTD)
                {
                    FoodPantryState state = new FoodPantryState();
                    state.Name = td.Text;
                    state.Url = td.GetAttribute("href");
                    stateList.Add(state);
                    log.LogInformation("successfully find state {0},", state.Name);
                    await FoodPantryStateCrawler.CrawlFoodPantrySateSiteAsync(state, driver, log);
                }

            }
            return stateList;
        }

        public static async Task Test(ILogger log)
        {
            //Create FoodPantry Container
            FoodPantryCityCrawler.FoodPantryContainer = await Common.CosmosDBUtils.CreateCosmosContainerAsync(FoodPantryCityCrawler.FoodPantryContainerId, FoodPantryCityCrawler.FoodPantryContainerPartionKey, log);
            if (FoodPantryCityCrawler.FoodPantryContainer != null)
            {
                log.LogInformation("Successfully created FoodPantryContainer");
            }

            FoodPantryState state = new FoodPantryState();
            state.Name = "Washington";
            state.Url = "http://bing.com";

            FoodPantryCity city = new FoodPantryCity();
            city.State = state;
            city.CityName = "Seattle";
            city.Url = "http://man.com";

            FoodPantry pantry = new FoodPantry();
            pantry.PantryName = "yumm Food";
            pantry.City = city;

            Common.CosmosDBUtils.AddItemToContainerAsync<FoodPantry>(pantry,
                FoodPantryCityCrawler.FoodPantryContainer,
                pantry.PantryName,
                pantry.City.CityName,
                log);
        }
    }
}
