using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Cryptocurrency.Storage.EntityConfigurations
{
    public class CryptocurrencyConfiguration : IEntityTypeConfiguration<Entities.Cryptocurrency>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Cryptocurrency> builder)
        {
            builder.ToTable("Cryptocurrency", "crypto");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.CoinMarketCapId);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Symbol).IsRequired().HasMaxLength(10);
            builder.Property(c => c.Rank);
            builder.Property(c => c.CirculatingSupply).HasColumnType("decimal(25,6)");
            builder.Property(c => c.TotalSupply).HasColumnType("decimal(25,6)");
            builder.Property(c => c.MaxSupply).HasColumnType("decimal(25,6)");
            builder.Property(c => c.Price).IsRequired().HasColumnType("decimal(25,6)");
            builder.Property(c => c.Volume24h).HasColumnType("decimal(25,6)");
            builder.Property(c => c.MarketCap).HasColumnType("decimal(25,6)");
            builder.Property(c => c.PercentChange1h).HasColumnType("decimal(25,6)");
            builder.Property(c => c.PercentChange24h).HasColumnType("decimal(25,6)");
            builder.Property(c => c.PercentChange7d).HasColumnType("decimal(25,6)");
            builder.Property(c => c.LastUpdateTime);

            builder.HasIndex(c => c.CoinMarketCapId);
            builder.HasIndex(c => c.Rank);
        }
    }
}
