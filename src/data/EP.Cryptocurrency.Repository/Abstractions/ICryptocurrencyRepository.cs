using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.Repository.Abstractions
{
    public interface ICryptocurrencyRepository : IRepository<Storage.Entities.Cryptocurrency>
    {
        Task<Storage.Entities.Cryptocurrency> FindByExternalKey(int coinMarketCapId);
    }
}
