using System;
using System.ComponentModel.DataAnnotations;

namespace EP.Cryptocurrency.Core.Models
{
    public class CryptocurrencyDetails
    {
        public CryptocurrencyDetails(Storage.Entities.Cryptocurrency cryptocurrency)
        {
            Id = cryptocurrency.Id;
            CoinMarketCapId = cryptocurrency.CoinMarketCapId;
            Name = cryptocurrency.Name;
            Symbol = cryptocurrency.Symbol;
            Rank = cryptocurrency.Rank;
            CirculatingSupply = cryptocurrency.CirculatingSupply;
            TotalSupply = cryptocurrency.TotalSupply;
            MaxSupply = cryptocurrency.MaxSupply;
            Price = cryptocurrency.Price;
            Volume24h = cryptocurrency.Volume24h;
            MarketCap = cryptocurrency.MarketCap;
            PercentChange1h = cryptocurrency.PercentChange1h;
            PercentChange24h = cryptocurrency.PercentChange24h;
            PercentChange7d = cryptocurrency.PercentChange7d;
            LastUpdateTime = cryptocurrency.LastUpdateTime;
        }

        public int Id { get; }
        public int CoinMarketCapId { get; }
        public string Name { get; }
        public string Symbol { get; }
        public int Rank { get; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? CirculatingSupply { get; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? TotalSupply { get; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? MaxSupply { get; }
        [DisplayFormat(DataFormatString = "{0:n6}")]
        public decimal Price { get; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? Volume24h { get; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? MarketCap { get; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? PercentChange1h { get; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? PercentChange24h { get; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? PercentChange7d { get; }
        public DateTime LastUpdateTime { get; }
    }
}
