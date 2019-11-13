using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Implementations;
using EP.Cryptocurrency.DataSupplier.Models;
using EP.Cryptocurrency.Repository.Abstractions;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EP.Cryptocurrency.DataSupplier.Tests
{
    public class DataSupplierTests
    {
        //private static readonly Mock<ICoinMarketCapService> coinMarketCapServiceMock = new Mock<ICoinMarketCapService>();
        //private static readonly Mock<ICryptocurrencyRepository> cryptoRepositoryMock = new Mock<ICryptocurrencyRepository>();
        //private static readonly Mock<ICryptocurrencyMapper> cryptoMapper = new Mock<ICryptocurrencyMapper>();
         
        //[Fact]
        //public async Task ReturnsCorrectDeserializedDataFromValidFile()
        //{
        //    coinMarketCapServiceMock.Setup(c => c.GetLatestListings(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Listing>() { 
        //    new Listing
        //    {
        //        Id = 1,
        //        CirculatingSupply = 1000000,
        //        LastUpdated = DateTime.UtcNow,
        //        MaxSupply = 10000000,
        //        Name = "Test",
        //        Rank = 1,
        //        Symbol = "TST",
        //        TotalSupply = 100000000,
        //        Quote = new Quote
        //        {
        //            Usd = new Currency
        //            {
        //                MarketCap = 100000000,
        //                PercentChange1h = 0.00001m,
        //                PercentChange24h = -0.00023m,
        //                PercentChange7d = 0.002012m,
        //                Price =5432.21305m,
        //                Volume24h = 100000m
        //            }
        //        }
        //    }
        //    });

        //    cryptoMapper.Setup(c => c.Map(It.IsAny<Listing>())).Returns(new Storage.Entities.Cryptocurrency
        //    {
        //        CoinMarketCapId = 1,
        //        CirculatingSupply = 1000000,
        //        LastUpdateTime = DateTime.UtcNow,
        //        MaxSupply = 10000000,
        //        Name = "Test",
        //        Rank = 1,
        //        Symbol = "TST",
        //        MarketCap = 100000000,
        //        PercentChange1h = 0.00001m,
        //        PercentChange24h = -0.00023m,
        //        PercentChange7d = 0.002012m,
        //        Price = 5432.21305m,
        //        Volume24h = 100000m,
        //        TotalSupply = 100000000,
        //    });

        //    cryptoRepositoryMock.Setup(c => c.Add(It.IsAny<Storage.Entities.Cryptocurrency>()));
        //    cryptoRepositoryMock.Setup(c => c.Update(It.IsAny<Storage.Entities.Cryptocurrency>()));

        //    var externalApiDataSupplier = new ExternalApiDataSupplier(coinMarketCapServiceMock.Object, cryptoMapper.Object, cryptoRepositoryMock.Object);

        //    await externalApiDataSupplier.Supply();

        //}

        //[Fact]
        //public async Task ReturnsCorrectStatusDataFromValidFile()
        //{
        //    coinMarketCapServiceMock.Setup(c => c.GetLatestListings()).ReturnsAsync(File.ReadAllText(@"TestData\latestListings.json"));
        //    var externalApiDataSupplier = new ExternalApiDataSupplier(coinMarketCapServiceMock.Object, cryptoRepositoryMock.Object);

        //    var latestListings = await externalApiDataSupplier.Supply();

        //    Assert.Equal(0, latestListings.Status.ErrorCode);
        //    Assert.Null(latestListings.Status.ErrorMessage);
        //    Assert.NotEmpty(latestListings.Listings);
        //}


        //[Fact]
        //public async Task ReturnsCorrectListingsDataFromValidFile()
        //{
        //    coinMarketCapServiceMock.Setup(c => c.GetLatestListings()).ReturnsAsync(File.ReadAllText(@"TestData\latestListings.json"));
        //    var externalApiDataSupplier = new ExternalApiDataSupplier(coinMarketCapServiceMock.Object, cryptoRepositoryMock.Object);

        //    var latestListings = await externalApiDataSupplier.Supply();
            
        //    Assert.NotEmpty(latestListings.Listings);
        //}

        //[Fact]
        //public async Task ReturnsErrorCodeAndMesageFromFileWithIncorrectStatus()
        //{
        //    coinMarketCapServiceMock.Setup(c => c.GetLatestListings()).ReturnsAsync(File.ReadAllText(@"TestData\incorrectStatus.json"));
        //    var externalApiDataSupplier = new ExternalApiDataSupplier(coinMarketCapServiceMock.Object, cryptoRepositoryMock.Object);

        //    var latestListings = await externalApiDataSupplier.Supply();

        //    Assert.NotEqual(0, latestListings.Status.ErrorCode);
        //    Assert.NotNull(latestListings.Status.ErrorMessage);
        //}
    }
}
