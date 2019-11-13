using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EP.Cryptocurrency.DataSupplier.Models
{
    [JsonObject]
    public class Status
    {
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty(PropertyName = "error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty(PropertyName = "error_message")]
        public string ErrorMessage { get; set; }
        [JsonProperty(PropertyName = "elapsed")]
        public int Elapsed { get; set; }
        [JsonProperty(PropertyName = "credit_count")]
        public int CreditCount { get; set; }
    }
}
