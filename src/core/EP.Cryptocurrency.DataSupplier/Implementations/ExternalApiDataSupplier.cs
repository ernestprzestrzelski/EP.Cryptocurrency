using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Models;
using EP.Cryptocurrency.Repository.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EP.Cryptocurrency.DataSupplier.Implementations
{
    public class ExternalApiDataSupplier : ICryptocurrencyDataSupplier
    {
        private readonly ICoinMarketCapService _coinMarketCapService;
        private readonly ICryptocurrencyMapper _cryptocurrencyMapper;
        private readonly ICryptocurrencyRepository _repository;

        private static ConcurrentBag<int> existingCoinMarketCapIds = new ConcurrentBag<int>();

        public ExternalApiDataSupplier(ICoinMarketCapService coinMarketCapService, ICryptocurrencyMapper cryptocurrencyMapper, ICryptocurrencyRepository repository)
        {
            _coinMarketCapService = coinMarketCapService ?? throw new ArgumentNullException(nameof(coinMarketCapService));
            _cryptocurrencyMapper = cryptocurrencyMapper ?? throw new ArgumentNullException(nameof(cryptocurrencyMapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task Supply()
        {
            foreach (Listing listing in await _coinMarketCapService.GetLatestListings().ConfigureAwait(false))
            {
                Storage.Entities.Cryptocurrency cryptoItem = _cryptocurrencyMapper.Map(listing);
                if(existingCoinMarketCapIds.Contains(cryptoItem.CoinMarketCapId))
                {
                    await _repository.Update(cryptoItem).ConfigureAwait(false);
                }
                else
                {
                    await _repository.Add(cryptoItem).ConfigureAwait(false);
                    existingCoinMarketCapIds.Add(cryptoItem.CoinMarketCapId);
                }
            }
        }

        static string makeAPICall()
        {
            var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "5000";
            queryString["convert"] = "USD";
            queryString["aux"] = "cmc_rank,max_supply,circulating_supply,total_supply";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", "78930229-3a63-4f13-8c21-9347e864e36a");
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());

        }
    }
    #region example
    class CSharpExample
    {
        private static string API_KEY = "78930229-3a63-4f13-8c21-9347e864e36a";

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(makeAPICall());
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static string makeAPICall()
        {
            var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "5000";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());

        }

    }
    #endregion
}
