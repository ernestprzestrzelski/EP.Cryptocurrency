using Newtonsoft.Json;

namespace EP.Cryptocurrency.DataSupplier.Models
{
    [JsonObject]
    public class Quote
    {
        [JsonProperty(PropertyName = "USD")]
        public Currency Usd { get; set; }
    }
}
