using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.DependencyInjection.AspNetCoreTesting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace BackendAPI3.Test
{
    public class API3ControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {

        //internal readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _factory;
        public static string Key { get; } = Guid.NewGuid().ToString("N");
        public API3ControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_count_result_success()
        {
            //Arrange
            string api3Url = "api/API3/Get";
            var client = _factory.CreateClient();
            //Act
            var toast = client.GetAsync(api3Url);
            var response1 = await toast;
            var responseData = await response1.Content.ReadFromJsonAsync<List<Address>>();
            //Assert            
            Assert.NotNull(responseData);
            Assert.Equal(4, responseData.Count());
        }

        [Theory]
        [InlineData("44153 Paget Ter", "Ashburn", "VA", "20147")]
        public async Task Should_post_success(string street, string city, string state, string zipCode)
        {
            //Arrange
            string api3Url = "api/API3/Send";
            var client = _factory.CreateClient();
            var json = System.Text.Json.JsonSerializer.Serialize(new Address() { Street= street, City=city, State=state, ZipCode=zipCode });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //Act            
            var response = await client.PostAsync(api3Url, content);
            var responseData = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Message from API3: I have got your post!", responseData);
        }

        [Fact]
        public async Task Should_respond_a_status_200_OK()
        {
            // Arrange
            string api3Url = "api/API3/Get";
            var client = _factory.CreateClient();

            // Act
            var toast = client.GetAsync(api3Url);
            //await Task.WhenAll(toast);
            var response1 = await toast;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
        }

    }

    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureWebHost(webHostBuilder => webHostBuilder
            .UseTestServerAndAddDefaultHttpClient()
            .UseStartup<AspNetCoreStartup>());
        private class AspNetCoreStartup
        {
            // public void ConfigureServices(IServiceCollection services) => services.AddLogging(lb => lb.AddXunitOutput());

            public void Configure(IApplicationBuilder app) =>
                app.Run(static context => context.Response.WriteAsync(API3ControllerTest.Key));
        }

    }

}