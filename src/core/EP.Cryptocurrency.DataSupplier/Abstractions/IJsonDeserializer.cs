using EP.Cryptocurrency.DataSupplier.Models;

namespace EP.Cryptocurrency.DataSupplier.Abstractions
{
    public interface IJsonDeserializer
    {
        T Deserialize<T>(string data) where T : LatestListingsResponse;
    }
}
