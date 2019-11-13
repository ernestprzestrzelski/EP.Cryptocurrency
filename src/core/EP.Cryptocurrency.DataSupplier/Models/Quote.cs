using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EP.Cryptocurrency.DataSupplier.Models
{
    [JsonObject]
    public class Quote
    {
        [JsonProperty(PropertyName = "USD")]
        public Currency Usd { get; set; }
    }
}
