using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CrawlerFunctions
{
    public class FoodPantryCity
    {
        private string _cityname;
        private string _url;
        private FoodPantryState _state;
        private IList<FoodPantry> _pantries;
        public FoodPantryCity()
        {
        }
        public FoodPantryCity(string cityname) => CityName = cityname;

        [JsonProperty(PropertyName = "partition")]
        public string CityName
        {
            get => _cityname;
            set => _cityname = value;
        }
        public string Url
        {
            get => _url;
            set => _url = value;
        }
        public FoodPantryState State
        { 
            get => _state;
            set => _state = value;
        }
        public IList<FoodPantry> Pantries
        {
            get
            {
                if (_pantries == null)
                    _pantries = new List<FoodPantry>();
                return _pantries;
            }
            set => _pantries = value;
        }
    }
}
