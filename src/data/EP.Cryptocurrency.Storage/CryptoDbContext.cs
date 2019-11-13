using Microsoft.EntityFrameworkCore;

namespace EP.Cryptocurrency.Storage
{
    public class CryptoDbContext : DbContext
    {
        public CryptoDbContext(DbContextOptions<CryptoDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Entities.Cryptocurrency> Cryptocurrencies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
