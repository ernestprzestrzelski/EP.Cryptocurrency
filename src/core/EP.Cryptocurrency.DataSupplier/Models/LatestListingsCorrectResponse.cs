using Newtonsoft.Json;
using System.Collections.Generic;

namespace EP.Cryptocurrency.DataSupplier.Models
{
    [JsonObject]
    public class LatestListingsCorrectResponse : LatestListingsResponse
    {

        [JsonProperty(PropertyName = "data")]
        public IEnumerable<CryptocurrencyListing> Listings { get; set; }
    }
}
