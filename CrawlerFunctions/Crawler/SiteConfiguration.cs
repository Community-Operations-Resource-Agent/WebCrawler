using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerFunctions.Crawler
{
    public class SiteConfiguration
    {
        // 3 levels:  State, City, FoodBank - hard coding to foodpantries.org for now
        public string[] Selectors = new string[] { "StateSelector", "City Selector", "FoodPantry Selector" };

        /// <summary>
        /// The level of the site that we're on
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The URL to crawl
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// The category of the site, like FoodPantry, Shelter, etc.
        /// </summary>
        public string Category { get; set; }

        public SiteConfiguration(int level, string url, string category)
        {
            Level = level;
            URL = url;
            Category = category;
        }
    }
}
