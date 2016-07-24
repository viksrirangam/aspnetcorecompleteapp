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
    public class RepositoryTests : DbContextBase
    {
        [TestMethod]
        public void CustomerRepositoryTest()
        {
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
