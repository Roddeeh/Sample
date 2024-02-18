using AutoFixture;
using FluentAssertions;
using Moq;
using Sample.Application.Clients;
using Sample.Application.Models;
using System.Net;

namespace Sample_Tests.Clients
{
    public class IndependentReserveClient_
    {
        private Mock<IHttpClientFactory> _httpClientFactoryMock;
        private IndependentReserveClient _client;
        private Fixture _fixture;

        public IndependentReserveClient_()
        {
            _fixture = new Fixture();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _client = new IndependentReserveClient(_httpClientFactoryMock.Object);
        }

        public async Task Should_GetRecentTrade_ValidResponse()
        {
            // Arrange
            var response = _fixture.Create<RecentTrade>();
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<RecentTrade>(response, new System.Net.Http.Formatting.JsonMediaTypeFormatter())
            };
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>())).ReturnsAsync(httpResponse);

            _httpClientFactoryMock.Setup(f => f.CreateClient()).Returns(httpClientMock.Object);

            // Act
            var result = await _client.GetRecentTrade("xbt", "aud", 10);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
