namespace CrawlerFunctions.Providers
{
    using System.Collections.Generic;
    using CrawlerFunctions.Models;
    using OpenQA.Selenium;
    using System;
    using OpenQA.Selenium.Chrome;
    using Microsoft.Extensions.Logging;

    public class HomelessShelterDirectorySiteCrawler : IShelterCrawler
    {
        private readonly ChromeDriver driver;
        private readonly ILogger log;
        private readonly IDatastoreProvider dbProvider;
        private string url = "https://www.homelessshelterdirectory.org/";

        public HomelessShelterDirectorySiteCrawler(ChromeDriver driver, IDatastoreProvider dbProvider, ILogger log)
        {
            this.driver = driver;
            this.log = log;
            this.dbProvider = dbProvider;

        }

        public void Crawl()
        {
            IList<ListingInformation> stateList = this.CrawlMainPage();
            IList<ListingInformation> cityList = this.CrawlStatePages(stateList);
            IList<ListingInformation> shelterList = this.CrawlCityPages(cityList);
        }

        /// <summary>
        /// Crawl main page and get state links
        /// </summary>
        /// <returns></returns>
        private IList<ListingInformation> CrawlMainPage()
        {
            IList<ListingInformation> stateList = new List<ListingInformation>();
            driver.Navigate().GoToUrl(@url);

            IWebElement selectElement = driver.FindElementById("states_home_search");
            IList<IWebElement> optionElements = selectElement.FindElements(By.TagName("option"));
            foreach (IWebElement element in optionElements)
            {
                if (String.IsNullOrEmpty(element.GetAttribute("value")))
                {
                    continue;
                }

                ListingInformation state = new ListingInformation
                {
                    Name = element.Text,
                    Url = GetAbsoluteUrl(url, element.GetAttribute("value"))
                };
                if (!state.IsEmpty())
                {
                    stateList.Add(state);
                }
                
            }

            return stateList;
        }


        /// <summary>
        /// Crawl state pages
        /// </summary>
        /// <param name="stateList"></param>
        /// <returns></returns>
        private IList<ListingInformation> CrawlStatePages(IList<ListingInformation> stateList)
        {
            IList<ListingInformation> cities = new List<ListingInformation>();

            foreach (var state in stateList)
            {
                try
                {
                    driver.Navigate().GoToUrl(state.Url);

                    IWebElement tripleElement = driver.FindElementById("triple");

                    /*
                    IList<IWebElement> cityElements = tripleElement.FindElements(By.XPath("//li/a"));
                    foreach (IWebElement cityElement in cityElements)
                    {
                        ListingInformation city = new ListingInformation
                        {
                            Name = cityElement.Text,
                            Url = cityElement.GetAttribute("href")
                        };
                        cities.Add(city);

                    }*/


                    IList<IWebElement> lis = tripleElement.FindElements(By.TagName("li"));
                    foreach (IWebElement li in lis)
                    {
                        IWebElement ele = li.FindElement(By.TagName("a"));
                        ListingInformation city = new ListingInformation
                        {
                                Name = ele.Text,
                                Url = ele.GetAttribute("href")
                        };
                        if (!city.IsEmpty())
                        {
                            cities.Add(city);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.LogError(ex, $"Exception while crawling shelter in state {state.Name} and url {state.Url}");
                }
            }
            return cities;
        }


        /// <summary>
        /// Crawl city pages
        /// </summary>
        /// <param name="cityList"></param>
        /// <returns></returns>
        private IList<ListingInformation> CrawlCityPages(IList<ListingInformation> cityList)
        {
            IList<ListingInformation> shelters = new List<ListingInformation>();

            foreach (var city  in cityList)
            {
                try
                {
                    driver.Navigate().GoToUrl(city.Url);

                    IList<IWebElement> h4Element = driver.FindElementsById("h4");
                    foreach (IWebElement element in h4Element)
                    {
                        IWebElement ele = element.FindElement(By.TagName("a"));
                        ListingInformation shelter = new ListingInformation
                        {
                            Name = ele.Text,
                            Url = ele.GetAttribute("href")
                        };
                        if (!shelter.IsEmpty())
                        {
                            shelters.Add(shelter);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.LogError(ex, $"Exception while crawling shelter in state {city.Name} and url {city.Url}");
                }
            }
            return shelters;
        }

        /// <summary>
        /// Crawl shelter pages
        /// </summary>
        /// <param name="shelterList"></param>
        /// <returns></returns>
        private IList<Shelter> CrawlShelterPages(IList<ListingInformation> shelterList)
        {
            IList<Shelter> shelters = new List<Shelter>();
            foreach (var shelterPage in shelterList)
            {
                //TO-DO crawl shelter pages and populate Shelter object
            }

            return shelters;
        }


        /// <summary>
        /// Persist shelter information
        /// </summary>
        /// <param name="shelters"></param>
        private void PersistShelterInformation(IList<Shelter> shelters)
        {
            //TO-DO
            //Get existing documents and update them with latest information
            //persist documents in bulk instead of one by one
            foreach (var shelter in shelters)
            {
                dbProvider.InsertDocumentAsync(shelter);
            }
        }


        /// <summary>
        /// Get absolute url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        private string GetAbsoluteUrl(string url, string part)
        {
            if (url == null || part == null)
            {
                return null;
            }

            if (part.StartsWith("http"))
            {
                return part;
            }
            return url.EndsWith('/') ? String.Concat(url, part) : String.Concat(url, "/", part);
        }

    }
}
