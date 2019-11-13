using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Models;
using EP.Cryptocurrency.Repository.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.DataSupplier.Implementations
{
    public class ExternalApiDataSupplier : ICryptocurrencyDataSupplier
    {
        private readonly ICoinMarketCapService _coinMarketCapService;
        private readonly ICryptocurrencyMapper _cryptocurrencyMapper;
        private readonly ICryptocurrencyRepository _repository;

        public ExternalApiDataSupplier(ICoinMarketCapService coinMarketCapService, ICryptocurrencyMapper cryptocurrencyMapper, ICryptocurrencyRepository repository)
        {
            _coinMarketCapService = coinMarketCapService ?? throw new ArgumentNullException(nameof(coinMarketCapService));
            _cryptocurrencyMapper = cryptocurrencyMapper ?? throw new ArgumentNullException(nameof(cryptocurrencyMapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task Supply()
        {
            var cryptoListings = await _coinMarketCapService.GetLatestListings().ConfigureAwait(false);
            
            if (!cryptoListings.Any())
                return;
            var itemsInDb = (await _repository.GetAll().ConfigureAwait(false)).ToList();

            foreach (CryptocurrencyListing listing in cryptoListings)
            {
                Storage.Entities.Cryptocurrency cryptoItem = _cryptocurrencyMapper.Map(listing);
                var itemInDb = itemsInDb.SingleOrDefault(i => i.CoinMarketCapId.Equals(cryptoItem.CoinMarketCapId));
                if(itemInDb != null)
                {
                    if (itemInDb.Equals(cryptoItem))
                        continue;
                    cryptoItem.Id = itemInDb.Id;
                    await _repository.Update(cryptoItem).ConfigureAwait(false);
                }
                else
                {
                    await _repository.Add(cryptoItem).ConfigureAwait(false);
                }
            }
        }
    }
}
