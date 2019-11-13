using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Implementations;
using EP.Cryptocurrency.Repository.Abstractions;
using EP.Cryptocurrency.Repository.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.Synchronizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var serviceCollection = new ServiceCollection().AddLogging(options => options.AddConsole())
                .AddTransient<ICryptocurrencyDataSupplier, ExternalApiDataSupplier>()
                .AddTransient<ICoinMarketCapService, CoinMarketCapService>()
                .AddTransient<ICryptocurrencyMapper, CryptocurrencyMapper>()
                .AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>()
                .AddDbContext<Storage.CryptoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CryptocurrencyDatabase")));
                serviceCollection.AddHttpClient("CoinMarketClient",
                options =>
                {
                    options.BaseAddress = new Uri(configuration.GetSection("CoinMarketCapApi:BaseAddress").Value);
                    options.DefaultRequestHeaders.Accept.Clear();
                    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    options.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", configuration["API_KEY"]);
                });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dataSupplier = serviceProvider.GetService<ICryptocurrencyDataSupplier>();
            do
            {
                Console.WriteLine("Press 'ENTER' to download data from CoinMarketCap API. Press 'ESC' to exit application ...");
                var pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Downloading data now ...");
                    await dataSupplier.Supply();
                    Console.WriteLine("Data downloaded correctly!");
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("You pressed 'ESC'. Exiting application now ...");
                    Thread.Sleep(1000);
                    break;
                }
            } while (true);
        }
    }
}
