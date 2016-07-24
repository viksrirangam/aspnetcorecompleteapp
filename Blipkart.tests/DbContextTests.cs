using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Blipkart.Model;

namespace BlipkartTest
{
    [TestClass]
    public class DbContextTests
    {
        private DbContextOptions<ShoppingContext> _contextOptions;

        public DbContextTests()
        {
            // Create a service provider to be shared by all test methods
            var serviceProvider = new ServiceCollection()
                 .AddEntityFrameworkInMemoryDatabase()
                 .BuildServiceProvider();

            // Create options telling the context to use an
            // InMemory database and the service provider.
            var builder = new DbContextOptionsBuilder<ShoppingContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            _contextOptions = builder.Options;

            // Insert the seed data that is expected by all test methods
            using (var context = new ShoppingContext(_contextOptions))
            {
                context.Customers.Add(new Customer(){Name = "Vikram"});

                context.Products.Add(new Product { Name = "Colgate", UnitPrice = 140.00 });
                context.Products.Add(new Product { Name = "Pepsodent", UnitPrice = 120.00 });
                context.Products.Add(new Product { Name = "DantKanti", UnitPrice = 160.00 });

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void DbContextTestMethodProducts()
        {
            using (var context = new ShoppingContext(_contextOptions))
            {
                var result = context.Products.ToList();
                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public void DbContextTestMethodCustomer()
        {
            using (var context = new ShoppingContext(_contextOptions))
            {
                var result = context.Customers.FirstOrDefault(c => c.Name == "Vikram");

                Assert.IsInstanceOfType(result, typeof(AuditableEntity<long>));
                Assert.AreEqual(result.CreatedBy, "blipkart");
            }
        }
    }
}
