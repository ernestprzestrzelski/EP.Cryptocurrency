using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.Core.Abstractions
{
    public interface ICryptocurrencyService
    {
        Task<IQueryable<Models.Cryptocurrency>> GetList<TKey>(Expression<Func<Storage.Entities.Cryptocurrency, TKey>> orderBy);

        Task<Models.CryptocurrencyDetails> GetDetails(int id);
    }
}
