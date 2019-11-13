using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EP.Cryptocurrency.DataSupplier.Models
{
    [JsonObject]
    public class LatestListingsResponse
    {
        [JsonProperty(PropertyName = "status")]
        public Status Status { get; set; }
    }
}
