using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace CrawlerFunctions
{
    public class FoodPantry
    {
        [JsonProperty(PropertyName = "id")]
        public string PantryName;
        public string Url;
        public string Descritpion;
        public string PostalCode;
        public string StreetAddress;
        public string Phone;
        public FoodPantryCity City;
    }
}
