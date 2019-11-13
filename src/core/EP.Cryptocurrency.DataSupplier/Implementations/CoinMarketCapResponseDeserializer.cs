using System;
using System.IO;
using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Models;
using Newtonsoft.Json;

namespace EP.Cryptocurrency.DataSupplier.Implementations
{
    public class CoinMarketCapResponseDeserializer : JsonSerializer, IJsonDeserializer
    {
        public T Deserialize<T>(string data) where T : LatestListingsResponse
        {
            using var reader = new StringReader(data);
            return (T)Deserialize(reader, typeof(T));
        }
    }
}
