using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Helpers;
using EP.Cryptocurrency.DataSupplier.Models;
using System;

namespace EP.Cryptocurrency.DataSupplier.Implementations
{
    public class CryptocurrencyMapper : ICryptocurrencyMapper
    {
        private readonly IMapperConstantsProvider _constantsProvider;

        public CryptocurrencyMapper(IMapperConstantsProvider constantsProvider)
        {
            _constantsProvider = constantsProvider ?? throw new ArgumentNullException(nameof(constantsProvider));
        }
        public Storage.Entities.Cryptocurrency Map(CryptocurrencyListing value)
        {
            if (value.Quote?.Usd == null)
                throw new ArgumentException($"USD quote should be present in {nameof(CryptocurrencyListing)}");

            return new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = value.Id,
                CirculatingSupply = value.CirculatingSupply.HasValue ? 
                Math.Round(value.CirculatingSupply.Value, _constantsProvider.RoundingNumberOfDecimalPlaces) as decimal? : null,
                LastUpdateTime = value.LastUpdated,
                MarketCap = value.Quote.Usd.MarketCap.HasValue ? 
                Math.Round(value.Quote.Usd.MarketCap.Value, _constantsProvider.RoundingNumberOfDecimalPlaces) as decimal? : null,
                MaxSupply = value.MaxSupply.HasValue ? 
                Math.Round(value.MaxSupply.Value, _constantsProvider.RoundingNumberOfDecimalPlaces) as decimal? : null,
                Name = value.Name,
                PercentChange1h = value.Quote.Usd.PercentChange1h.HasValue ? 
                Math.Round(value.Quote.Usd.PercentChange1h.Value, _constantsProvider.RoundingNumberOfDecimalPlaces) as decimal? : null,
                PercentChange24h = value.Quote.Usd.PercentChange24h.HasValue ? 
                Math.Round(value.Quote.Usd.PercentChange24h.Value, _constantsProvider.RoundingNumberOfDecimalPlaces) as decimal? : null,
                PercentChange7d = value.Quote.Usd.PercentChange7d.HasValue ? 
                Math.Round(value.Quote.Usd.PercentChange7d.Value, _constantsProvider.RoundingNumberOfDecimalPlaces) as decimal? : null,
                Price = Math.Round(value.Quote.Usd.Price, _constantsProvider.RoundingNumberOfDecimalPlaces),
                Rank = value.Rank,
                Symbol = value.Symbol,
                TotalSupply = value.TotalSupply.HasValue ? 
                Math.Round(value.TotalSupply.Value, _constantsProvider.RoundingNumberOfDecimalPlaces) as decimal? : null,
                Volume24h = value.Quote.Usd.Volume24h.HasValue ? 
                Math.Round(value.Quote.Usd.Volume24h.Value, _constantsProvider.RoundingNumberOfDecimalPlaces) as decimal? : null
            };
        }
    }
}
