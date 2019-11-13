using EP.Cryptocurrency.DataSupplier.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.DataSupplier.Abstractions
{
    public interface ICoinMarketCapService
    {
        Task<IEnumerable<Listing>> GetLatestListings(int start = 1, int limit = 5000);
    }
}
