using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerFunctions
{
    public class FoodPantryState
    {
        private string _name;
        private string _url;
        private IList<FoodPantryCity> _cities;
        public FoodPantryState() { }
        public FoodPantryState(string name) => Name = name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        //        public string ID;
        public string Url
        {
            get => _url;
            set => _url = value;
        }
        public IList<FoodPantryCity> Cities
        {
            get => _cities;
            set => _cities = value;
        }

    }
}
