using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Blipkart;
using Blipkart.Service;
using Blipkart.ViewModel;
using Blipkart.Core;
using Blipkart.Controllers;

namespace BlipkartTest
{
    [TestClass]
    [Ignore]
    public class ControllerTests
    {
        [TestMethod]
        public void Index_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            var stubService = new Mock<IProductService>();
            var mockLogger = new Mock<ILogger<HomeController>>();

            stubService.Setup(repo => repo.GetItems()).Returns(GetSampleItems());
            //mockLogger.Setup(x => x.LogInformation(It.IsAny<int>(), It.IsAny<string>()));

            var controller = new HomeController(stubService.Object, mockLogger.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var model = (IEnumerable<Item>)((ViewResult)result).ViewData.Model;
            Assert.AreEqual(2, model.Count());

            var view  = result as ViewResult;
            //ViewName is empty.
            //Assert.AreEqual("Index", view.ViewName);

            //stubService.Verify(x => x.GetItems());
            //not working extention methods cannot be veified.
            //mockLogger.Verify(x => x.LogInformation(new EventId(LoggingEvents.LIST_ITEMS, null), "Listing all items"));
            //mockLogger.Verify(x => x.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items"));
        }

        private List<Item> GetSampleItems()
        {
            var products = new List<Item>();
            products.Add(new Item()
            {
                ProductId = 1221,
                ProductName = "Colgate",
                Price = 120.00
            });
            products.Add(new Item()
            {
                ProductId = 1222,
                ProductName = "Pepsodent",
                Price = 140.00
            });

            return products;
        }
    }
}
