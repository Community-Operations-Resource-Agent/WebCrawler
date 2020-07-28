using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerFunctions
{
    public class FoodPantryCity
    {
        private string _name;
        private string _url;
        private string _stateName;
        private IList<FoodPantryCity> _pantries;
        public FoodPantryCity() { }
        public FoodPantryCity(string name) => Name = name;

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
        public string StateName
        {
            get => _stateName;
            set => _stateName = value;
        }
        public IList<FoodPantryCity> Pantries
        {
            get => _pantries;
            set => _pantries = value;
        }
    }
}
