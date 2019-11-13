using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Cryptocurrency.DataSupplier.Helpers
{
    public interface ICoinMarketHttpClientParametersProvider
    {
        string Name { get; }
        string GetLatestListingsFullRequestUri { get; }
    }
}
