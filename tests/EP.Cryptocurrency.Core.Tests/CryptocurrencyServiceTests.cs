using EP.Cryptocurrency.Core.Implementations;
using EP.Cryptocurrency.Repository.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EP.Cryptocurrency.Core.Tests
{
    public class CryptocurrencyServiceTests
    {
        private static readonly Mock<ICryptocurrencyRepository> cryptoRepoMock = new Mock<ICryptocurrencyRepository>();

        private static readonly IQueryable<Storage.Entities.Cryptocurrency> validCryptoTwoElementsCollection = new List<Storage.Entities.Cryptocurrency>()
            {
                new Storage.Entities.Cryptocurrency
                {
                    CoinMarketCapId = 2,
                    Id = 2,
                    LastUpdateTime = DateTime.Now,
                    Name = "test1",
                    Price = 1000m,
                    Rank = 1,
                    Symbol = "TST"
                },
                 new Storage.Entities.Cryptocurrency
                {
                    CoinMarketCapId = 1,
                    Id = 1,
                    LastUpdateTime = DateTime.Now,
                    Name = "test2",
                    Price = 2000m,
                    Rank = 2,
                    Symbol = "TES"
                }
            }.AsQueryable();

        [Fact]
        public async Task GetListReturnsListOrderedByRankWhenRankOrderingFunctionPassed()
        {
            cryptoRepoMock.Setup(c => c.GetAll()).ReturnsAsync(validCryptoTwoElementsCollection);
            var service = new CryptocurrencyService(cryptoRepoMock.Object);

            var orderedList = await service.GetList(c => c.Rank);

            Assert.Equal(2, orderedList.ElementAt(0).CoinMarketCapId);
            Assert.Equal(1, orderedList.ElementAt(1).CoinMarketCapId);
        }
    }
}
