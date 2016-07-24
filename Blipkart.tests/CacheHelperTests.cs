using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Blipkart.Model;
using Blipkart.Core.Infra;

namespace BlipkartTest
{
    [TestClass]
    public class CacheHelperTests
    {
        [TestMethod]
        public void CacheHelperTest()
        {
            // Arrange
            var product = new Product()
                            {
                                Id = 1221,
                                Name = "Rexona",
                                UnitPrice = 35.00
                            };
            Object p;
            var stubEntry = new Mock<ICacheEntry>();
            var stubMemoryCache = new Mock<IMemoryCache>();
            stubMemoryCache.Setup(s => s.TryGetValue(It.IsAny<Object>(), out p)).Returns(false);
            stubMemoryCache.Setup(s => s.CreateEntry(It.IsAny<Object>())).Returns(stubEntry.Object);
            stubMemoryCache.Setup(s => s.Remove(It.IsAny<Object>()));

            // Act
            var cache = new CacheHelper<Product>(stubMemoryCache.Object);

            Product _p;
            var exists = cache.TryGet("key1", out _p);
            Assert.IsFalse(exists);
            Assert.IsNull(_p);

            cache.Set("key1", product);
            cache.Remove("key1");

            stubMemoryCache.Verify(s => s.CreateEntry(It.IsAny<string>()));
            stubMemoryCache.Verify(s => s.Remove(It.IsAny<string>()));
        }
    }
}
