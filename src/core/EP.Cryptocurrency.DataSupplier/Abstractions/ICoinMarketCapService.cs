using EP.Cryptocurrency.DataSupplier.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.DataSupplier.Abstractions
{
    public interface ICoinMarketCapService
    {
        Task<IEnumerable<CryptocurrencyListing>> GetLatestListings();
    }
}
