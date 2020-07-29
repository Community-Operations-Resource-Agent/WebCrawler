using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CrawlerFunctions
{
    public static class FoodPantryCityCrawler
    {
        public static async Task CrawlFoodPantryCityWebSite(
           ChromeDriver driver,
           FoodPantryCity city)
        {

            driver.Navigate().GoToUrl(city.Url);
            IList<IWebElement> tableElements = driver.FindElements(By.XPath("//script[@type='application/ld+json']"));
            foreach (IWebElement block in tableElements)
            {
                string html = block.GetAttribute("innerHTML");
                if (html.Contains(@"postalCode") && html.Contains(city.Name))
                {
                    city.Pantries.Add(ProcessFoodPantry(html, city.Name));
                }

            }
        }

        public static FoodPantry ProcessFoodPantry(string html, string city)
        {
            FoodPantry pantry = new FoodPantry();
            string processhtml = html.Replace(System.Environment.NewLine, @"");
            string[] parts = processhtml.Split(@",");
            foreach (string part in parts)
            {
                string[] pair = part.Split(@":");
                if (pair[0].Contains("name"))
                    pantry.Name = pair[1];
            }
            return pantry;
        }
    }
}
