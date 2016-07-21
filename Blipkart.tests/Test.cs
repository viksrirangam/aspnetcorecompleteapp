using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Blipkart;

namespace mstests
{
    [TestClass]
    public class TestClass
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TestClass()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestMethod]
        public void TestMethodPassing()
        {
          Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestMethodFailing()
        {
          Assert.IsTrue(false);
        }

        [TestMethod]
        public async void HomePageTest()
        {
            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual("Hello World!", responseString);
        }
        /*
        [TestMethod]
        public async Task Index_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetTestSessions()));
            var controller = new HomeController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }*/
    }
}
