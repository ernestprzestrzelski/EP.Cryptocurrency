using EP.Cryptocurrency.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.Repository.Implementations
{
    public class CryptocurrencyRepository : Repository<Storage.Entities.Cryptocurrency>, ICryptocurrencyRepository
    {
        public CryptocurrencyRepository(Storage.CryptoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Storage.Entities.Cryptocurrency> FindByExternalKey(int coinMarketCapId)
        {
            return await _dbContext.Set<Storage.Entities.Cryptocurrency>().FirstOrDefaultAsync(c => c.CoinMarketCapId.Equals(coinMarketCapId));
        }
    }
}
