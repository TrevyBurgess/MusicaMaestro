using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyberFeedForward.MusicaMaestro.Services;

namespace CyberFeedForward.MusicaMaestro.Tests.Services;

[TestClass]
public class AiServiceTests
{
    private class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public HttpRequestMessage? LastRequest { get; private set; }

        public TestHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            return Task.FromResult(_response);
        }
    }

    [TestMethod]
    public async Task GenerateAsync_ReturnsContent_WhenResponseContainsChoices()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("{\"choices\":[{\"message\":{\"content\":\"C major scale\"}}]}")
        };

        var service = new AiService(new HttpClient(new TestHttpMessageHandler(response)));
        var result = await service.GenerateAsync("compose a melody", "http://localhost/v1", "model", "key");

        Assert.AreEqual("C major scale", result);
    }

    [TestMethod]
    public async Task GenerateAsync_AddsAuthorizationHeader_WhenApiKeyProvided()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("{\"choices\":[{\"message\":{\"content\":\"ok\"}}]}")
        };

        var handler = new TestHttpMessageHandler(response);

        var service = new AiService(new HttpClient(handler));
        _ = await service.GenerateAsync("hello", "http://api.openai.com/v1", "gpt-4", "sk-test");

        Assert.IsNotNull(handler.LastRequest);
        Assert.AreEqual("Bearer sk-test", handler.LastRequest.Headers.Authorization?.ToString());
    }

    [TestMethod]
    public async Task GenerateAsync_Throws_WhenEndpointIsEmpty()
    {
        var service = new AiService(new HttpClient());

        await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
            service.GenerateAsync("prompt", string.Empty, "model", string.Empty));
    }
}
