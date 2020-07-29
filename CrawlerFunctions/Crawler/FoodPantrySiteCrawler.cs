using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CrawlerFunctions
{
    public static class FoodPantrySiteCrawler
    {
        private static string FoodPantrySiteURL = @"https://www.foodpantries.org/";
        private static string FoodPantryContainerId = @"FoodPantryContainer";
        public static Container FoodPantryContainer;
        public static async Task<IList<FoodPantryState>> CrawlFoodPantryWebSiteAsync(ChromeDriver driver, ILogger log)
        {
            //Create FoodPantry Container
            FoodPantryContainer = await Common.CosmosDBUtils.CreateCosmosContainerAsync(FoodPantryContainerId, log);
            if (FoodPantryContainer != null)
            {
                log.LogInformation("Successfully created FoodPantryContainer");
            }

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
                }

            }
            return stateList;
        }
    }
}
