using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Blipkart.ViewModel;

namespace BlipkartTest
{
    [TestClass]
    public class ViewModelTests
    {
        //async test methods are not supported.
        [TestMethod]
        public async void TestMethodFailingAsync()
        {
            var result = await Task.Run(()=> {return false;});
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CartViewModelTest()
        {
            var cart = new Cart(){
                CustomerId = 1221,
                CustomerName = "Stranger",
                CartId = Guid.NewGuid().ToString()
            };

            Assert.AreEqual(cart.GrandTotal, 0.00);
            Assert.IsTrue(cart.IsEmpty);

            cart.LineItems.Add(new LineItem(){
                ProductId = 1221,
                ProductName = "Colgate",
                Price = 120.00,
                Quantity = 4
            });

            cart.LineItems.Add(new LineItem(){
                ProductId = 1222,
                ProductName = "Pepsodent",
                Price = 140.00,
                Quantity = 1
            });

            Assert.AreEqual(cart.LineItems[0].Total, 480.00);
            Assert.IsFalse(cart.IsEmpty);
            Assert.AreEqual(cart.LineItems[1].Total, 140.00);

            Assert.AreEqual(cart.GrandTotal, 620.00);
        }

        [TestMethod]
        public void OrderInfoViewModelTest()
        {
            var order = new OrderInfo(){
                Id = 1221,
                OrderDate = DateTime.Now
            };

            Assert.AreEqual(order.GrandTotal, 0.00);

            order.LineItems.Add(new LineItem(){
                ProductId = 1221,
                ProductName = "Colgate",
                Price = 120.00,
                Quantity = 4
            });

            order.LineItems.Add(new LineItem(){
                ProductId = 1222,
                ProductName = "Pepsodent",
                Price = 140.00,
                Quantity = 1
            });

            Assert.AreEqual(order.GrandTotal, 620.00);
        }
    }
}
