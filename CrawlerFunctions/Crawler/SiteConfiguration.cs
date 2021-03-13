using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CrawlerFunctions.Crawler
{
    public class SiteConfiguration
    {

        // SiteURL, pageURL
        // Add a clone the creates a new object copying over key stuff

        /// <summary>
        /// The next parsing level of the site
        /// TODO: Ask questions about what this is?
        /// </summary>
        public int NextLevel { get; set; }

        /// <summary>
        /// The name of the website (organization, nonprofit, church, etc)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The URL of the top level site
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// The URL of the page that we need to crawl
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// The category of the site, like FoodPantry, Shelter, etc.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The list of selectors for the site, down to the deepest level
        /// </summary>
        public List<SiteSelector> SiteSelectors { get; set; }

        public SiteConfiguration()
        {
        }

        /// <summary>
        /// Method used to copy the current site collection into a new object, setup the next page to crawl and increment the level
        /// </summary>
        /// <param name="nextPageUrl">The next page that needs to be crawled</param>
        /// <returns></returns>
        public SiteConfiguration CreateNext(string nextName, string nextPageUrl)
        {
            return new SiteConfiguration()
            {
                NextLevel = this.NextLevel + 1,
                Name = nextName,
                SiteUrl = this.SiteUrl,
                PageUrl = nextPageUrl,
                Category = this.Category,
                SiteSelectors = this.SiteSelectors
            };
        }
    }

    public class SiteSelector
    {
        public int Level { get; set; }
        public string Type { get; set; }
        public string GroupSelector { get; set; }
        public string NameProperty { get; set; }
        public string NamePropertyTextRemove { get; set; }
        public string LinkProperty { get; set; }

        public override String ToString()
        {
            return Level + " " + Type + " " + GroupSelector + " " + NameProperty + " " + NamePropertyTextRemove + " " + LinkProperty;
        }
    }
}
