using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Blipkart;

namespace BlipkartTest
{
    [TestClass]
    [Ignore]
    public class IntegratedTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IntegratedTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestMethod]
        public void HomePageTest()
        {
            // Act
            var response = _client.GetAsync("/").Result;
            response.EnsureSuccessStatusCode();

            var responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.AreNotEqual("Hello World!", responseString);
        }
    }
}
