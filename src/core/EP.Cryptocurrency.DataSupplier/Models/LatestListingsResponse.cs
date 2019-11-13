using Newtonsoft.Json;

namespace EP.Cryptocurrency.DataSupplier.Models
{
    [JsonObject]
    public class LatestListingsResponse
    {
        [JsonProperty(PropertyName = "status")]
        public Status Status { get; set; }
    }
}
