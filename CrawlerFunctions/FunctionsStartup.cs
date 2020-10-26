using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ScrapySharp.Network;
using System.IO;

[assembly: FunctionsStartup(typeof(CrawlerFunctions.Startup))]

namespace CrawlerFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ScrapingBrowser>((s) => {
                return new ScrapingBrowser();
            });

        }

        //public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        //{
        //    FunctionsHostBuilderContext context = builder.GetContext();

        //    builder.ConfigurationBuilder
        //        .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
        //        .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
        //        .AddEnvironmentVariables();
        //}
    }
}
