using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;

namespace CrawlerFunctions
{
    public static class FoodPantrySiteCrawler
    {
        public static async Task<IList<FoodPantryState>> CrawlFoodPantryWebSite(ChromeDriver driver)
        {
            IList<FoodPantryState> stateList = new List<FoodPantryState>();
            driver.Navigate().GoToUrl(@"https://www.foodpantries.org/");

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
