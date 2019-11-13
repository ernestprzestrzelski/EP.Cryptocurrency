using EP.Cryptocurrency.Core.Abstractions;
using EP.Cryptocurrency.Core.Models;
using EP.Cryptocurrency.Repository.Abstractions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.Core.Implementations
{
    public class CryptocurrencyService : ICryptocurrencyService
    {
        private readonly ICryptocurrencyRepository _repository;

        public CryptocurrencyService(ICryptocurrencyRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IQueryable<Models.Cryptocurrency>> GetList<TKey>(Expression<Func<Storage.Entities.Cryptocurrency, TKey>> orderBy)
        {
            return (await _repository.GetAll().ConfigureAwait(false))
                .OrderBy(orderBy)
                .Select(crypto => new Models.Cryptocurrency(crypto));
        }
        public async Task<CryptocurrencyDetails> GetDetails(int id)
        {
            return new CryptocurrencyDetails(await _repository.Find(id).ConfigureAwait(false));                
        }
    }
}
