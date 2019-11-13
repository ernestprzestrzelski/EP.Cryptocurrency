using System;

namespace EP.Cryptocurrency.DataSupplier.Helpers
{
    public class CoinMarketHttpClientParametersProvider : ICoinMarketHttpClientParametersProvider
    {
        private readonly ICoinMarketQueryParametersProvider _queryParametersProvider;

        public CoinMarketHttpClientParametersProvider(ICoinMarketQueryParametersProvider queryParametersProvider)
        {
            _queryParametersProvider = queryParametersProvider ?? throw new ArgumentNullException(nameof(queryParametersProvider));
        }

        private const string LatestListingsUrl = "v1/cryptocurrency/listings/latest";
        public string Name => "CoinMarketClient";
        public string GetLatestListingsFullRequestUri => $"{LatestListingsUrl}{_queryParametersProvider.Provide()}";
    }
}
