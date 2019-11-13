using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EP.Cryptocurrency.DataSupplier.Models
{
    [JsonObject]
    public class Currency
    {
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
        [JsonProperty(PropertyName = "volume_24h")]
        public decimal? Volume24h { get; set; }
        [JsonProperty(PropertyName = "market_cap")]
        public decimal? MarketCap { get; set; }
        [JsonProperty(PropertyName = "percent_change_1h")]
        public decimal? PercentChange1h { get; set; }
        [JsonProperty(PropertyName = "percent_change_24h")]
        public decimal? PercentChange24h { get; set; }
        [JsonProperty(PropertyName = "percent_change_7d")]
        public decimal? PercentChange7d { get; set; }
    }
}
