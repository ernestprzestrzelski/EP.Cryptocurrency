using System;
using System.ComponentModel.DataAnnotations;

namespace EP.Cryptocurrency.Core.Models
{
    public class Cryptocurrency
    {
        internal Cryptocurrency(Storage.Entities.Cryptocurrency cryptocurrency)
        {
            Id = cryptocurrency.Id;
            CoinMarketCapId = cryptocurrency.CoinMarketCapId;
            Name = cryptocurrency.Name;
            Symbol = cryptocurrency.Symbol;
            Price = cryptocurrency.Price;
            Volume24h = cryptocurrency.Volume24h;
            MarketCap = cryptocurrency.MarketCap;
            PercentChange24h = cryptocurrency.PercentChange24h;
        }

        public int Id { get; }
        public int CoinMarketCapId { get; }
        public string Name { get; }
        public string Symbol { get; }
        [DisplayFormat(DataFormatString = "{0:n6}")]
        public decimal Price { get; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? Volume24h { get; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? MarketCap { get; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? PercentChange24h { get; }
    }
}
