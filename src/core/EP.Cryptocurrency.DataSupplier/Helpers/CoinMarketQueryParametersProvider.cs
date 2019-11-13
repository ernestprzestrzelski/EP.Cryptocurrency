using System.Web;

namespace EP.Cryptocurrency.DataSupplier.Helpers
{
    public class CoinMarketQueryParametersProvider : ICoinMarketQueryParametersProvider
    {
        private const string StartParameterName = "start";
        private const string LimitParameterName = "limit";
        private const string QuoteParameterName = "convert";
        private const string MetadataParameterName = "aux";
        private const string StartParameterValue = "1";
        private const string LimitParameterValue = "5000";
        private const string QuoteParameterValue = "USD";
        private const string MetadataParameterValue = "cmc_rank,max_supply,circulating_supply,total_supply";

        public string Provide()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString[StartParameterName] = StartParameterValue;
            queryString[LimitParameterName] = LimitParameterValue;
            queryString[QuoteParameterName] = QuoteParameterValue;
            queryString[MetadataParameterName] = MetadataParameterValue;
            return $"?{queryString}";
        }
    }
}
