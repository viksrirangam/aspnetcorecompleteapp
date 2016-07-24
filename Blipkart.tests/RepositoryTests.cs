using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Blipkart.Model;
using Blipkart.Repository;

namespace BlipkartTest
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void CustomerRepositoryTest()
        {
            var data = new List<Product>
            {
                new Product { Name = "BBB", UnitPrice = 10.00 },
                new Product { Name = "ZZZ", UnitPrice = 11.00 },
                new Product { Name = "AAA", UnitPrice = 12.00 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ShoppingContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var repo = new ProductRepository(mockContext.Object);
            Assert.IsNotNull(repo, "repo is null");

            var products = repo.GetAll().ToList();
            Assert.AreEqual(3, products.Count);

            var bbb = repo.GetById(1);
            Assert.IsNotNull(bbb, "object is null");
            Assert.AreEqual("BBB", bbb.Name);
        }
    }
}
