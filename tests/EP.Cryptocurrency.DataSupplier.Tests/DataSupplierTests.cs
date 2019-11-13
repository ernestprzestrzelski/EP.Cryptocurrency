using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Implementations;
using EP.Cryptocurrency.DataSupplier.Models;
using EP.Cryptocurrency.Repository.Abstractions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EP.Cryptocurrency.DataSupplier.Tests
{
    public class DataSupplierTests
    {
        private readonly Mock<ICoinMarketCapService> coinMarketCapServiceMock = new Mock<ICoinMarketCapService>();
        private readonly Mock<ICryptocurrencyRepository> cryptoRepositoryMock = new Mock<ICryptocurrencyRepository>();
        private readonly Mock<ICryptocurrencyMapper> cryptoMapper = new Mock<ICryptocurrencyMapper>();


        [Fact]
        public async Task SupplyShouldReturnBeforeGettingDbItemsIfGetLatestListingsReturnsEmptyCollection()
        {
            coinMarketCapServiceMock.Setup(c => c.GetLatestListings()).ReturnsAsync(Enumerable.Empty<CryptocurrencyListing>());
            var supplier = new ExternalApiDataSupplier(coinMarketCapServiceMock.Object, cryptoMapper.Object, cryptoRepositoryMock.Object);

            await supplier.Supply();
            cryptoRepositoryMock.Verify(c => c.GetAll(), Times.Never);

        }

        [Fact]
        public async Task SupplyShouldCallRepositoryAddIfListingsAreNotInCache()
        {
            var cryptoList = new List<CryptocurrencyListing>
            { 
                new CryptocurrencyListing
                {
                    Id = 1
                }, 
                new CryptocurrencyListing
                {
                    Id = 2
                }
            };
            coinMarketCapServiceMock.Setup(c => c.GetLatestListings()).ReturnsAsync(cryptoList);
            cryptoRepositoryMock.Setup(c => c.Add(It.IsAny<Storage.Entities.Cryptocurrency>()));
            cryptoMapper.SetupSequence(c => c.Map(It.IsAny<CryptocurrencyListing>())).Returns(new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = 1
            }).Returns(new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = 2
            });

            var supplier = new ExternalApiDataSupplier(coinMarketCapServiceMock.Object, cryptoMapper.Object, cryptoRepositoryMock.Object);

            await supplier.Supply();

            cryptoRepositoryMock.Verify(c => c.Add(It.IsAny<Storage.Entities.Cryptocurrency>()), Times.Exactly(cryptoList.Count));
        }

        [Fact]
        public async Task SupplyShouldNotCallRepositoryUpdateIfListingsAreInCacheAndItemIsNotChanged()
        {
            var cryptoList = new List<CryptocurrencyListing>
            {
                new CryptocurrencyListing
                {
                    Id = 1
                },
                new CryptocurrencyListing
                {
                    Id = 2
                }
            };
            coinMarketCapServiceMock.Setup(c => c.GetLatestListings()).ReturnsAsync(cryptoList);
            cryptoRepositoryMock.Setup(c => c.Add(It.IsAny<Storage.Entities.Cryptocurrency>()));
            cryptoRepositoryMock.Setup(c => c.Update(It.IsAny<Storage.Entities.Cryptocurrency>()));
            cryptoMapper.SetupSequence(c => c.Map(It.IsAny<CryptocurrencyListing>())).Returns(new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = 1
            }).Returns(new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = 1
            });

            var supplier = new ExternalApiDataSupplier(coinMarketCapServiceMock.Object, cryptoMapper.Object, cryptoRepositoryMock.Object);

            await supplier.Supply();
            cryptoRepositoryMock.Verify(c => c.Update(It.IsAny<Storage.Entities.Cryptocurrency>()), Times.Never);

        }

        [Fact]
        public async Task SupplyShouldCallRepositoryUpdateIfListingsAreInCacheButItemIsChanged()
        {
            var cryptoList = new List<CryptocurrencyListing>
            {
                new CryptocurrencyListing
                {
                    Id = 1,
                    LastUpdated = new System.DateTime(2019, 11, 12, 11, 11, 11)
                },
                new CryptocurrencyListing
                {
                    Id = 2,
                    LastUpdated = new System.DateTime(2019, 11, 12, 10, 10, 10)
                }
            };
            var dbCryptoList = new List<Storage.Entities.Cryptocurrency>
            {
                new Storage.Entities.Cryptocurrency
                {
                    CoinMarketCapId = 1,
                    LastUpdateTime = new System.DateTime(2019, 11, 12, 11, 11, 11)
                },
                new Storage.Entities.Cryptocurrency
                {
                    CoinMarketCapId = 2,
                    LastUpdateTime = new System.DateTime(2019, 11, 12, 10, 10, 10)
                }
            };
            coinMarketCapServiceMock.Setup(c => c.GetLatestListings()).ReturnsAsync(cryptoList);
            cryptoRepositoryMock.Setup(c => c.Add(It.IsAny<Storage.Entities.Cryptocurrency>()));
            cryptoRepositoryMock.SetupSequence(c => c.GetAll()).ReturnsAsync(Enumerable.Empty<Storage.Entities.Cryptocurrency>().AsQueryable())
                .ReturnsAsync(dbCryptoList.AsQueryable());
            cryptoRepositoryMock.Setup(c => c.Update(It.IsAny<Storage.Entities.Cryptocurrency>()));
            cryptoMapper.SetupSequence(c => c.Map(It.IsAny<CryptocurrencyListing>())).Returns(new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = 1,
                LastUpdateTime = new System.DateTime(2019, 11, 12, 11, 11, 11)
            }).Returns(new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = 2,
                LastUpdateTime = new System.DateTime(2019, 11, 12, 12, 12, 12)
            }).Returns(new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = 1,
                LastUpdateTime = new System.DateTime(2019, 11, 12, 11, 11, 11)
            }).Returns(new Storage.Entities.Cryptocurrency
            {
                CoinMarketCapId = 2,
                LastUpdateTime = new System.DateTime(2019, 11, 12, 12, 12, 12)
            });

            var supplier = new ExternalApiDataSupplier(coinMarketCapServiceMock.Object, cryptoMapper.Object, cryptoRepositoryMock.Object);

            await supplier.Supply();
            await supplier.Supply();
            cryptoRepositoryMock.Verify(c => c.Update(It.IsAny<Storage.Entities.Cryptocurrency>()), Times.Once);

        }

    }
}
