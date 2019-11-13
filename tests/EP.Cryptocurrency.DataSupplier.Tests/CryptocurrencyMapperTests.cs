using EP.Cryptocurrency.DataSupplier.Helpers;
using EP.Cryptocurrency.DataSupplier.Implementations;
using Moq;
using System;
using Xunit;

namespace EP.Cryptocurrency.DataSupplier.Tests
{
    public class CryptocurrencyMapperTests
    {
        private static readonly Mock<IMapperConstantsProvider> constantsProviderMock = new Mock<IMapperConstantsProvider>();

        [Fact]
        public void MapShouldThrowArgumentExceptionIfQuoteIsNull()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);

            Assert.Throws<ArgumentException>(() => mapper.Map(new Models.CryptocurrencyListing()));
        }

        [Fact]
        public void MapShouldRoundCirculatingSupply()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);
            var circulatingSupply = 100000.123456789m;

            Storage.Entities.Cryptocurrency mappedItem = mapper.Map(new Models.CryptocurrencyListing
            {
                CirculatingSupply = circulatingSupply,
                Quote = new Models.Quote
                {
                    Usd = new Models.Currency()
                }
            });

            Assert.NotEqual(circulatingSupply, mappedItem.CirculatingSupply);
            Assert.Equal(Math.Round(circulatingSupply, precision), mappedItem.CirculatingSupply);
        }

        [Fact]
        public void MapShouldRoundTotalSupply()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);
            var totalSupply = 100000.123456789m;

            Storage.Entities.Cryptocurrency mappedItem = mapper.Map(new Models.CryptocurrencyListing
            {
                TotalSupply = totalSupply,
                Quote = new Models.Quote
                {
                    Usd = new Models.Currency()
                }
            });

            Assert.NotEqual(totalSupply, mappedItem.TotalSupply);
            Assert.Equal(Math.Round(totalSupply, precision), mappedItem.TotalSupply);
        }

        [Fact]
        public void MapShouldRoundMaxSupply()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);
            var maxSupply = 100000.123456789m;

            Storage.Entities.Cryptocurrency mappedItem = mapper.Map(new Models.CryptocurrencyListing
            {
                MaxSupply = maxSupply,
                Quote = new Models.Quote
                {
                    Usd = new Models.Currency()
                }
            });

            Assert.NotEqual(maxSupply, mappedItem.MaxSupply);
            Assert.Equal(Math.Round(maxSupply, precision), mappedItem.MaxSupply);
        }

        [Fact]
        public void MapShouldRoundMarketCap()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);
            var marketCap = 100000.123456789m;

            Storage.Entities.Cryptocurrency mappedItem = mapper.Map(new Models.CryptocurrencyListing
            {
                Quote = new Models.Quote
                {
                    Usd = new Models.Currency
                    {
                        MarketCap = marketCap
                    }
                }
            });

            Assert.NotEqual(marketCap, mappedItem.MarketCap);
            Assert.Equal(Math.Round(marketCap, precision), mappedItem.MarketCap);
        }

        [Fact]
        public void MapShouldRoundPercentChange1h()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);
            var percentChange1h = 100000.123456789m;

            Storage.Entities.Cryptocurrency mappedItem = mapper.Map(new Models.CryptocurrencyListing
            {
                Quote = new Models.Quote
                {
                    Usd = new Models.Currency
                    {
                        PercentChange1h = percentChange1h
                    }
                }
            });

            Assert.NotEqual(percentChange1h, mappedItem.PercentChange1h);
            Assert.Equal(Math.Round(percentChange1h, precision), mappedItem.PercentChange1h);
        }

        [Fact]
        public void MapShouldRoundPercentChange24h()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);
            var percentChange24h = 100000.123456789m;

            Storage.Entities.Cryptocurrency mappedItem = mapper.Map(new Models.CryptocurrencyListing
            {
                Quote = new Models.Quote
                {
                    Usd = new Models.Currency
                    {
                        PercentChange24h = percentChange24h
                    }
                }
            });

            Assert.NotEqual(percentChange24h, mappedItem.PercentChange24h);
            Assert.Equal(Math.Round(percentChange24h, precision), mappedItem.PercentChange24h);
        }

        [Fact]
        public void MapShouldRoundPercentChange7d()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);
            var percentChange7d = 100000.123456789m;

            Storage.Entities.Cryptocurrency mappedItem = mapper.Map(new Models.CryptocurrencyListing
            {
                Quote = new Models.Quote
                {
                    Usd = new Models.Currency
                    {
                        PercentChange7d = percentChange7d
                    }
                }
            });

            Assert.NotEqual(percentChange7d, mappedItem.PercentChange7d);
            Assert.Equal(Math.Round(percentChange7d, precision), mappedItem.PercentChange7d);
        }

        [Fact]
        public void MapShouldRoundVolume24h()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);
            var volume24h = 100000.123456789m;

            Storage.Entities.Cryptocurrency mappedItem = mapper.Map(new Models.CryptocurrencyListing
            {
                Quote = new Models.Quote
                {
                    Usd = new Models.Currency
                    {
                        Volume24h = volume24h
                    }
                }
            });

            Assert.NotEqual(volume24h, mappedItem.Volume24h);
            Assert.Equal(Math.Round(volume24h, precision), mappedItem.Volume24h);
        }

        [Fact]
        public void MapShouldRoundPrice()
        {
            int precision = 6;
            constantsProviderMock.Setup(c => c.RoundingNumberOfDecimalPlaces).Returns(precision);
            var mapper = new CryptocurrencyMapper(constantsProviderMock.Object);
            var price = 100000.123456789m;

            Storage.Entities.Cryptocurrency mappedItem = mapper.Map(new Models.CryptocurrencyListing
            {
                Quote = new Models.Quote
                {
                    Usd = new Models.Currency
                    {
                        Price = price
                    }
                }
            });

            Assert.NotEqual(price, mappedItem.Price);
            Assert.Equal(Math.Round(price, precision), mappedItem.Price);
        }

    }
}
