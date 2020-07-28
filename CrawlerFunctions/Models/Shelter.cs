namespace CrawlerFunctions.Models
{

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents shelter information
    /// </summary>
    public class Shelter
    {
        /// <summary>
        /// Shelter id
        /// </summary>
        public string Id;
        
        /// <summary>
        /// Shelter name
        /// </summary>
        public string Name;

        /// <summary>
        /// Shelter website
        /// </summary>
        public string Url;

        /// <summary>
        /// Shelter timings
        /// </summary>
        public string OpenTime;

        /// <summary>
        /// Shelter Phone
        /// </summary>
        public string Phone;

        /// <summary>
        /// Shelter Address
        /// </summary>
        public string Address;

        /// <summary>
        /// Shelter state
        /// </summary>
        public string State;

        /// <summary>
        /// Shelter city
        /// </summary>
        public string City;

        /// <summary>
        /// Shelter country
        /// </summary>
        public string Country;

        /// <summary>
        /// Shelter description
        /// </summary>
        public string Description;

        /// <summary>
        /// Shelter type: Day Shelters, Emergency Homeless Shelters,
        /// Halfway Housing, Permanent Affordable Housing, Supportive Housing
        /// </summary>
        public string Type;

        /// <summary>
        /// Services available at the shelter
        /// for e.g. soup kitchen, medical services, counseling 
        /// </summary>
        public List<string> Services;

        /// <summary>
        /// Restrictions for e.g. men only, family
        /// </summary>
        public List<string> Restrictions;

        /// <summary>
        /// date when information
        /// was last updated
        /// </summary>
        public DateTime InformationLastUpdated;
    }
}
