using System;

namespace Crawl_foodpantries.org
{
    class Program
    {
        static void Main(string[] args)
        {

            // Need to use something like selenium, webkit.Net or other headless browser for C# to get webpages
            // In this case :
            //  1)  Get all the states
            //  2)  For each State, get all the food pantries
            //  3)  For each food pantry, grab the key information (URL, name, location, phone number, etc)
            //  4)  Save all the info as structured data in CosmosDB

            // Right now - just doing this as a console app, but will incorporate into the azure functions once it's working!

        }
    }
}
