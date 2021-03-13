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
        /// The URL to crawl
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// The category of the site, like FoodPantry, Shelter, etc.
        /// </summary>
        public string Category { get; set; }

        public List<SiteSelector> SiteSelectors { get; set; }

        public SiteConfiguration()
        {
        }

        public override String ToString()
        {
            return Name + " " + URL + " " + Category + " " + NextLevel;
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
