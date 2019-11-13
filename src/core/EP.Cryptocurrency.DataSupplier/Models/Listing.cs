using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EP.Cryptocurrency.DataSupplier.Models
{
    [JsonObject]
    public class Listing
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "max_supply")]
        public decimal? MaxSupply { get; set; }
        [JsonProperty(PropertyName = "circulating_supply")]
        public decimal? CirculatingSupply { get; set; }
        [JsonProperty(PropertyName = "total_supply")]
        public decimal? TotalSupply { get; set; }
        [JsonProperty(PropertyName = "cmc_rank")]
        public int Rank { get; set; }
        [JsonProperty(PropertyName = "last_updated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty(PropertyName = "quote")]
        public Quote Quote { get; set; }
    }
}
