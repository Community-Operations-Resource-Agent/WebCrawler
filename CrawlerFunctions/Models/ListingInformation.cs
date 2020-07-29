using System;

namespace CrawlerFunctions.Models
{
    public class ListingInformation
    {
        /// <summary>
        /// Resource name - city or state
        /// </summary>
        public string Name;

        /// <summary>
        /// Resource url
        /// </summary>
        public string Url;

        public ListingInformation()
        {

        }

        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) && String.IsNullOrEmpty(Url);
        }

    }
}
