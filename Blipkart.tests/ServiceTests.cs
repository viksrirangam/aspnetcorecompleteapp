using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Blipkart;
using Blipkart.Service;
using Blipkart.ViewModel;
using Blipkart.Core;
using Blipkart.Model;
using Blipkart.Repository;

namespace BlipkartTest
{
    [TestClass]
    [Ignore]
    public class ServiceTests
    {
        [TestMethod]
        public void CartServiceTest()
        {
            // Arrange
            Cart _c = new Cart();
            IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

            var sessionStub = new Mock<ISession>();
            sessionStub.SetupGet(s => s.Id).Returns("theblipkartcartid");

            var stubHttpContext = new Mock<HttpContext>();
            stubHttpContext.SetupGet(s => s.Session).Returns(sessionStub.Object);

            var stubHttpContextAccessor = new Mock<IHttpContextAccessor>();
            stubHttpContextAccessor.SetupGet(c => c.HttpContext).Returns(stubHttpContext.Object);

            var cartService = new CartService(_memoryCache, stubHttpContextAccessor.Object);

            // Act
            var sessionId = stubHttpContextAccessor.Object.HttpContext.Session.Id;
            Assert.IsNotNull(sessionId);

            // Assert
            var cart = cartService.GetCart();
            Assert.IsInstanceOfType(cart, typeof(Cart));
            Assert.AreEqual(cart.CartId, "theblipkartcartid");
        }

        /*
        [TestMethod]
        public void CartServiceTest()
        {
            // Arrange
            Cart _c = new Cart();
            IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
            Object _o;
            var stubCache = new Mock<IMemoryCache>();
            stubCache.Setup(s => s.TryGetValue(It.IsAny<object>(), out _o)).Returns(false);
            //stubCache.Setup(s => s.Set(It.IsAny<object>(), It.IsAny<Cart>())).Returns(_c);

            var sessionStub = new Mock<ISession>();
            sessionStub.SetupGet(s => s.Id).Returns("theblipkartcartid");

            var stubHttpContext = new Mock<HttpContext>();
            stubHttpContext.SetupGet(s => s.Session).Returns(sessionStub.Object);

            var stubHttpContextAccessor = new Mock<IHttpContextAccessor>();
            stubHttpContextAccessor.SetupGet(c => c.HttpContext).Returns(stubHttpContext.Object);

            var cartService = new CartService(stubCache.Object, stubHttpContextAccessor.Object);

            // Act
            //var cart = cartService.GetCart();
            var sessionId = stubHttpContextAccessor.Object.HttpContext.Session.Id;

            // Assert
            //Assert.IsInstanceOfType(cart, typeof(Cart));
            //Assert.AreEqual(cart.CartId, "theblipkartsessionid");
            Assert.IsNotNull(sessionId);

            //stubCache.Object.Set(sessionId, _c);
        }*/

        [TestMethod]
        public void ProductServiceTest()
        {
            // Arrange
            var stubRepo = new Mock<IProductRepository>();
            stubRepo.Setup(s => s.GetById(It.IsAny<long>())).Returns(new Product()
            {
                Id = 1221,
                Name = "Rexona",
                UnitPrice = 35.00
            });
            var stubUoW = new Mock<IUnitOfWork>();

            // Act
            IProductService service = new ProductService(stubUoW.Object, stubRepo.Object);
            Item item = service.GetById(1221);

            // Assert
            Assert.IsNotNull(item);
            Assert.AreEqual(1221, item.ProductId);
        }

        [TestMethod]
        public void ProductServiceGetAllTest()
        {
            // Arrange
            var stubUoW = new Mock<IUnitOfWork>();
            var stubRepo = new Mock<IProductRepository>();
            stubRepo.Setup(s => s.GetAll()).Returns(new List<Product>()
            {
                new Product(){
                    Id = 1221,
                    Name = "Rexona",
                    UnitPrice = 35.00
                },
                new Product(){
                    Id = 1222,
                    Name = "Lux",
                    UnitPrice = 39.00
                }
            });

            // Act
            IProductService service = new ProductService(stubUoW.Object, stubRepo.Object);
            var items = service.GetItems().ToList();

            // Assert
            Assert.IsNotNull(items);
            Assert.AreEqual(2, items.Count);
        }
    }
}
