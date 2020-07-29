using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;

namespace CrawlerFunctions
{
    public static class FoodPantryStateCrawler
    {
        public static async Task CrawlFoodPantrySateSiteAsync(ChromeDriver driver, FoodPantryState state)
        {
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
                    city.Name = td.Text;
                    city.Url = td.GetAttribute("href");
                    city.State = state;
                    cityList.Add(city);
                }

            }
            state.Cities = cityList;
            driver.Navigate().Back();
        }
    }
}
