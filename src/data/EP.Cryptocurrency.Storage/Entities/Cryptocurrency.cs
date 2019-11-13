using EP.Cryptocurrency.Storage.Abstractions;
using System;

namespace EP.Cryptocurrency.Storage.Entities
{
    public class Cryptocurrency : IEntity
    {
        public int Id { get; set; }
        public int CoinMarketCapId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Rank { get; set; }
        public decimal? CirculatingSupply { get; set; }
        public decimal? TotalSupply { get; set; }
        public decimal? MaxSupply { get; set; }
        public decimal Price { get; set; }
        public decimal? Volume24h { get; set; }
        public decimal? MarketCap { get; set; }
        public decimal? PercentChange1h { get; set; }
        public decimal? PercentChange24h { get; set; }
        public decimal? PercentChange7d { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Cryptocurrency cryptocurrency &&
                   CoinMarketCapId == cryptocurrency.CoinMarketCapId &&
                   LastUpdateTime == cryptocurrency.LastUpdateTime;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(CoinMarketCapId);
            hash.Add(Name);
            hash.Add(Symbol);
            hash.Add(Rank);
            hash.Add(CirculatingSupply);
            hash.Add(TotalSupply);
            hash.Add(MaxSupply);
            hash.Add(Price);
            hash.Add(Volume24h);
            hash.Add(MarketCap);
            hash.Add(PercentChange1h);
            hash.Add(PercentChange24h);
            hash.Add(PercentChange7d);
            hash.Add(LastUpdateTime);
            return hash.ToHashCode();
        }
    }
}
