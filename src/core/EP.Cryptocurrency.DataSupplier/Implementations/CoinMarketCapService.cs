using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EP.Cryptocurrency.DataSupplier.Implementations
{
    public class CoinMarketCapService : ICoinMarketCapService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private const string CoinMarketClient = "CoinMarketClient";
        private const string LatestListingsUrl = "v1/cryptocurrency/listings/latest";
        private const string StartParameter = "start";
        private const string LimitParameter = "limit";
        private const string QuoteParameterName = "convert";
        private const string MetadataParameterName = "aux";
        private const string QuoteParameterValue = "USD";
        private const string MetadataParameterValue = "cmc_rank,max_supply,circulating_supply,total_supply";
        public CoinMarketCapService(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            var loggerFactoryInstance = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactoryInstance.CreateLogger<CoinMarketCapService>();
            //_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Listing>> GetLatestListings(int start = 1, int limit = 5000)
        {
            var httpClient = _httpClientFactory.CreateClient(CoinMarketClient);
            var query = LatestListingsUrl + BuildQueryString(start, limit);
            HttpResponseMessage response = await httpClient.GetAsync(LatestListingsUrl+BuildQueryString(start, limit)).ConfigureAwait(false);
            var jsonData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jsonSerializer = new JsonSerializer(); 
            using (var reader = new StringReader(jsonData))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var deserializedStatus = (LatestListingsResponse)jsonSerializer.Deserialize(reader, typeof(LatestListingsResponse));
                    string message = $"{CoinMarketClient} returned '{response.StatusCode}' response with external status '{deserializedStatus.Status.ErrorCode}' and message '{deserializedStatus.Status.ErrorMessage}'";
                    _logger.LogWarning(message);
                    return Enumerable.Empty<Listing>();
                }
                else
                {
                    var deserializedData = (LatestListingsCorrectResponse)jsonSerializer.Deserialize(reader, typeof(LatestListingsCorrectResponse));
                    _logger.LogDebug($"{CoinMarketClient} returned correct response with data");
                    return deserializedData.Listings;
                }
            }
        }

        private string BuildQueryString(int start, int limit)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString[StartParameter] = start.ToString();
            queryString[LimitParameter] = limit.ToString();
            queryString[QuoteParameterName] = QuoteParameterValue;
            queryString[MetadataParameterName] = MetadataParameterValue;
            return $"?{queryString}";
        }
    }
}
