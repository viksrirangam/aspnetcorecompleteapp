using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Blipkart.Model;

namespace BlipkartTest
{
    public class DbContextBase
    {
        protected DbContextOptions<ShoppingContext> _contextOptions;

        public DbContextBase()
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
    }
}
