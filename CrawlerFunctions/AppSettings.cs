using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CrawlerFunctions
{
    public class AppSettings
    {
        public static string EndpointUrl { get; set; }
        public static string PrimaryKey { get; set; }

        public static void LoadAppSettings(string dir)
        {
            if (EndpointUrl == null)
            {
                IConfigurationRoot configRoot = new ConfigurationBuilder()
                    .SetBasePath(dir)
                    .AddJsonFile("local.settings.json")
                    .Build();
                EndpointUrl = configRoot["CosmosDB:EndpointUrl"];
                PrimaryKey = configRoot["CosmosDB:PrimaryKey"];
            }
        }
    }
}
