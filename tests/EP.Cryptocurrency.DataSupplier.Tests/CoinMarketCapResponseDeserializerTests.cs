using EP.Cryptocurrency.DataSupplier.Implementations;
using EP.Cryptocurrency.DataSupplier.Models;
using System;
using System.IO;
using Xunit;

namespace EP.Cryptocurrency.DataSupplier.Tests
{
    public class CoinMarketCapResponseDeserializerTests
    {
        [Fact]
        public void DeserializeShouldThrowArgumentNullExceptionWhenNullIsPassed()
        {
            var deserializer = new CoinMarketCapResponseDeserializer();
            Assert.Throws<ArgumentNullException>(() => deserializer.Deserialize<LatestListingsResponse>(null));
        }

        [Fact]
        public void DeserializeShouldReturnCorrectDataWhenValidJsonIsPassed()
        {
            var deserializer = new CoinMarketCapResponseDeserializer();
            LatestListingsCorrectResponse deserializedData = deserializer.Deserialize<LatestListingsCorrectResponse>
                (File.ReadAllText(@"TestData\latestListings.json"));
            Assert.NotNull(deserializedData);
            Assert.NotNull(deserializedData.Status);
            Assert.NotNull(deserializedData.Listings);
            Assert.NotEmpty(deserializedData.Listings);
        }
    }
}
