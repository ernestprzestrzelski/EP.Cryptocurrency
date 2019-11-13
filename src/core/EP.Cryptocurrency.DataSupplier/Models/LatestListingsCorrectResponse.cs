using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Cryptocurrency.DataSupplier.Models
{
    [JsonObject]
    public class LatestListingsCorrectResponse : LatestListingsResponse
    {

        [JsonProperty(PropertyName = "data")]
        public IEnumerable<Listing> Listings { get; set; }
    }
}
