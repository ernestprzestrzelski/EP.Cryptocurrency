using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Helpers;
using EP.Cryptocurrency.DataSupplier.Implementations;
using EP.Cryptocurrency.DataSupplier.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EP.Cryptocurrency.DataSupplier.Tests
{
    public class CoinMarketCapServiceTests
    {
        private static readonly Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();
        private static readonly Mock<ICoinMarketHttpClientParametersProvider> httpClientParamsProviderMock = new Mock<ICoinMarketHttpClientParametersProvider>(); 
        private static readonly Mock<IJsonDeserializer> jsonDeserializerMock = new Mock<IJsonDeserializer>();

        [Fact]
        public async Task GetLatestListingsShouldReturnEmptyEnumerableIfResponseIsNot200()
        {
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent("test")
            }));
            var client = new HttpClient(clientHandlerStub);
            httpClientFactoryMock.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(client);
            httpClientParamsProviderMock.Setup(c => c.Name).Returns("test");
            httpClientParamsProviderMock.Setup(c => c.GetLatestListingsFullRequestUri).Returns("http://test");
            jsonDeserializerMock.Setup(c => c.Deserialize<LatestListingsResponse>(It.IsAny<string>())).Returns(new LatestListingsResponse
            {
                Status = new Status
                {
                    ErrorCode = 401,
                    ErrorMessage = "API Key missing"
                }
            });

            var service = new CoinMarketCapService(httpClientFactoryMock.Object, httpClientParamsProviderMock.Object, 
                jsonDeserializerMock.Object, new NullLoggerFactory());

            var response = await service.GetLatestListings();

            Assert.Empty(response);
        }

        [Fact]
        public async Task GetLatestListingsShouldReturnValidCollectionIfResponseIs200()
        {
            var clientHandlerStub = new DelegatingHandlerStub();
            var client = new HttpClient(clientHandlerStub);
            httpClientFactoryMock.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(client);
            httpClientParamsProviderMock.Setup(c => c.Name).Returns("test");
            httpClientParamsProviderMock.Setup(c => c.GetLatestListingsFullRequestUri).Returns("http://test");
            jsonDeserializerMock.Setup(c => c.Deserialize<LatestListingsCorrectResponse>(It.IsAny<string>())).Returns(new LatestListingsCorrectResponse
            {
                Status = new Status
                {
                    ErrorCode = 0
                },
                Listings = new List<CryptocurrencyListing>
                {
                    new CryptocurrencyListing
                    {
                        Quote = new Quote
                        {
                            Usd = new Currency()
                        }
                    },
                    new CryptocurrencyListing()
                }
            });

            var service = new CoinMarketCapService(httpClientFactoryMock.Object, httpClientParamsProviderMock.Object,
                jsonDeserializerMock.Object, new NullLoggerFactory());

            var response = await service.GetLatestListings();

            Assert.NotEmpty(response);
        }

        internal class DelegatingHandlerStub : DelegatingHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
            public DelegatingHandlerStub()
            {
                _handlerFunc = (request, cancellationToken) => Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("test")
                });
            }

            public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
            {
                _handlerFunc = handlerFunc;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return _handlerFunc(request, cancellationToken);
            }
        }
    }
}
