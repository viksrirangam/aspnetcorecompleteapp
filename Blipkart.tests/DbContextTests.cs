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
    public class DbContextTests : DbContextBase
    {
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
