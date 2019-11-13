using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Helpers;
using EP.Cryptocurrency.DataSupplier.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.DataSupplier.Implementations
{
    public class CoinMarketCapService : ICoinMarketCapService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICoinMarketHttpClientParametersProvider _httpClientParametersProvider;
        private readonly IJsonDeserializer _coinMarketResponseDeserializer;
        private readonly ILogger _logger;
        public CoinMarketCapService(IHttpClientFactory httpClientFactory, ICoinMarketHttpClientParametersProvider httpClientParametersProvider, IJsonDeserializer coinMarketResponseDeserializer, ILoggerFactory loggerFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClientParametersProvider = httpClientParametersProvider ?? throw new ArgumentNullException(nameof(httpClientParametersProvider));
            _coinMarketResponseDeserializer = coinMarketResponseDeserializer ?? throw new ArgumentNullException(nameof(coinMarketResponseDeserializer));
            var loggerFactoryInstance = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactoryInstance.CreateLogger<CoinMarketCapService>();
        }

        public async Task<IEnumerable<CryptocurrencyListing>> GetLatestListings()
        {
            var httpClient = _httpClientFactory.CreateClient(_httpClientParametersProvider.Name);
            var response = await httpClient.GetAsync(_httpClientParametersProvider.GetLatestListingsFullRequestUri).ConfigureAwait(false);
            var jsonData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var deserializedStatus = _coinMarketResponseDeserializer.Deserialize<LatestListingsResponse>(jsonData);// (LatestListingsResponse)jsonSerializer.Deserialize(reader, typeof(LatestListingsResponse));
                string message = $"{_httpClientParametersProvider.Name} returned '{response.StatusCode}' response with external status '{deserializedStatus.Status.ErrorCode}' and message '{deserializedStatus.Status.ErrorMessage}'";
                _logger.LogWarning(message);
                return Enumerable.Empty<CryptocurrencyListing>();
            }
            else
            {
                var deserializedData = _coinMarketResponseDeserializer.Deserialize<LatestListingsCorrectResponse>(jsonData);// (LatestListingsCorrectResponse)jsonSerializer.Deserialize(reader, typeof(LatestListingsCorrectResponse));
                _logger.LogInformation($"{_httpClientParametersProvider.Name} returned correct response with data");
                return deserializedData.Listings;
            }
        }
    }
}
