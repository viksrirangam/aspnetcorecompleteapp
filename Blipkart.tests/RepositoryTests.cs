using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

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
            /*
            var data = new List<Product>
            {
                new Product { Id = 1, Name = "BBB", UnitPrice = 10.00 },
                new Product { Id = 2, Name = "ZZZ", UnitPrice = 11.00 },
                new Product { Id = 3, Name = "AAA", UnitPrice = 12.00 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ShoppingContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var repo = new ProductRepository(mockContext.Object);*/

            // Create a service provider to be shared by all test methods
            var serviceProvider = new ServiceCollection()
                 .AddEntityFrameworkInMemoryDatabase()
                 .BuildServiceProvider();

            // Create options telling the context to use an
            // InMemory database and the service provider.
            var builder = new DbContextOptionsBuilder<ShoppingContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            DbContextOptions<ShoppingContext> _contextOptions = builder.Options;

            // Insert the seed data that is expected by all test methods
            using (var context = new ShoppingContext(_contextOptions))
            {
                context.Customers.Add(new Customer(){Name = "Vikram"});

                context.Products.Add(new Product { Name = "Colgate", UnitPrice = 140.00 });
                context.Products.Add(new Product { Name = "Pepsodent", UnitPrice = 120.00 });
                context.Products.Add(new Product { Name = "DantKanti", UnitPrice = 160.00 });

                context.SaveChanges();
            }

            using (var context = new ShoppingContext(_contextOptions))
            {
                var repo = new ProductRepository(context);
                Assert.IsNotNull(repo, "repo is null");

                var products = repo.GetAll().ToList();
                Assert.AreEqual(3, products.Count);

                var colgate = repo.GetById(1);
                Assert.IsNotNull(colgate, "colgate is null");
                Assert.AreEqual("Colgate", colgate.Name);

                var dkanti = repo.GetById(3);
                Assert.IsNotNull(dkanti, "DantKanti is null");
                Assert.AreEqual("DantKanti", dkanti.Name);
            }
        }
    }
}
