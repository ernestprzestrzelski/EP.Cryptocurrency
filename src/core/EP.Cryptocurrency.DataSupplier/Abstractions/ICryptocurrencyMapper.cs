﻿using EP.Cryptocurrency.DataSupplier.Models;

namespace EP.Cryptocurrency.DataSupplier.Abstractions
{
    public interface ICryptocurrencyMapper
    {
        Storage.Entities.Cryptocurrency Map(CryptocurrencyListing value);
    }
}
