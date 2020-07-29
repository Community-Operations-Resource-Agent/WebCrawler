using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerFunctions
{
    public class FoodPantryCity
    {
        private string _name;
        private string _url;
        private FoodPantryState _state;
        private IList<FoodPantry> _pantries;
        public FoodPantryCity()
        {

        }
        public FoodPantryCity(string name) => name = name;

        public string Name
        {
            get => _name;
            set => _name = value;
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
            get => _pantries;
            set => _pantries = value;
        }
    }
}
