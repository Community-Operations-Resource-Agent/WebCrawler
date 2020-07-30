using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using CrawlerFunctions.Common;
using Microsoft.Azure.Cosmos;

namespace CrawlerFunctions
{
    public static class FoodPantryCityCrawler
    {
        public static string FoodPantryContainerId = @"FoodPantryContainer";
        public static string FoodPantryContainerPartionKey = @"/partition";
        public static Container FoodPantryContainer;

        public static async Task CrawlFoodPantryCityWebSite(
            FoodPantryCity city,
            ChromeDriver driver,
            ILogger log)
        {
            log.LogInformation("Start to crawl city {0} in state {1} web page", city.CityName, city.State.Name);

            driver.Navigate().GoToUrl(city.Url);
            IList<IWebElement> tableElements = driver.FindElements(By.XPath("//script[@type='application/ld+json']"));
            foreach (IWebElement block in tableElements)
            {
                string html = block.GetAttribute("innerHTML");
                if (html.Contains(@"postalCode") && html.Contains(city.CityName))
                {
                    FoodPantry pantry = await ProcessFoodPantryAsync(html, city, log);
                    city.Pantries.Add(pantry);
                }

            }
        }

        public static async Task<FoodPantry> ProcessFoodPantryAsync(string html, FoodPantryCity city, ILogger log)
        {
            //Create FoodPantry Container
            FoodPantryCityCrawler.FoodPantryContainer = await Common.CosmosDBUtils.CreateCosmosContainerAsync(FoodPantryCityCrawler.FoodPantryContainerId, FoodPantryCityCrawler.FoodPantryContainerPartionKey, log);
            if (FoodPantryCityCrawler.FoodPantryContainer != null)
            {
                log.LogInformation("Successfully created FoodPantryContainer");
            }
            FoodPantry pantry = new FoodPantry();
            pantry.City = city;
            StringBuilder builder = new StringBuilder(html);
            builder.Replace(System.Environment.NewLine, string.Empty);
            builder.Replace("\"", string.Empty);
            builder.Replace(@"\", string.Empty);

            string[] parts = builder.ToString().Split(@",");
            foreach (string part in parts)
            {
                string[] pair = part.Split(@":");
                if (pair[0].Contains("name"))
                    pantry.PantryName = pair[1];
                if (pair[0].Contains("description"))
                    pantry.PostalCode = pair[1];
                if (pair[0].Contains("postalCode"))
                    pantry.PostalCode = pair[1];
                if (pair[0].Contains("streetAddress"))
                    pantry.StreetAddress = pair[1];
                if (pair[0].Contains("telephone"))
                    pantry.Phone = pair[1];
            }
            Common.CosmosDBUtils.AddItemToContainerAsync<FoodPantry>(pantry,
                FoodPantryContainer,
                pantry.PantryName,
                pantry.City.CityName,
                log);
            return pantry;
        }
    }
}
