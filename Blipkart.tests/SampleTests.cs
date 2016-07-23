using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlipkartTest
{
    [TestClass]
    public class SampleTests
    {
        [TestMethod]
        public void TestMethodPassing()
        {
          Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestMethodFailing()
        {
          Assert.IsFalse(false);
        }

        //async test methods are not supported.
        [TestMethod]
        public async void TestMethodFailingAsync()
        {
            var result = await Task.Run(()=> {return false;});
            Assert.IsFalse(result);
        }
    }
}
