using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Models;
using EP.Cryptocurrency.Repository.Abstractions;
using EP.Cryptocurrency.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.DataSupplier.Implementations
{
    public class CryptocurrencyMapper : ICryptocurrencyMapper
    {
        public Storage.Entities.Cryptocurrency Map(Listing value)
        {
            return new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = value.Id,
                CirculatingSupply = value.CirculatingSupply.HasValue ? Math.Round(value.CirculatingSupply.Value, 6) as decimal? : null,
                LastUpdateTime = value.LastUpdated,
                MarketCap = value.Quote.Usd.MarketCap.HasValue ? Math.Round(value.Quote.Usd.MarketCap.Value, 6) as decimal? : null,
                MaxSupply = value.MaxSupply.HasValue ? Math.Round(value.MaxSupply.Value, 6) as decimal? : null,
                Name = value.Name,
                PercentChange1h = value.Quote.Usd.PercentChange1h.HasValue ? Math.Round(value.Quote.Usd.PercentChange1h.Value, 6) as decimal? : null,
                PercentChange24h = value.Quote.Usd.PercentChange24h.HasValue ? Math.Round(value.Quote.Usd.PercentChange24h.Value, 6) as decimal? : null,
                PercentChange7d = value.Quote.Usd.PercentChange7d.HasValue ? Math.Round(value.Quote.Usd.PercentChange7d.Value, 6) as decimal? : null,
                Price = Math.Round(value.Quote.Usd.Price, 6),
                Rank = value.Rank,
                Symbol = value.Symbol,
                TotalSupply = value.TotalSupply.HasValue ? Math.Round(value.TotalSupply.Value, 6) as decimal? : null,
                Volume24h = value.Quote.Usd.Volume24h.HasValue ? Math.Round(value.Quote.Usd.Volume24h.Value, 6) as decimal? : null
            };
        }
    }
}
