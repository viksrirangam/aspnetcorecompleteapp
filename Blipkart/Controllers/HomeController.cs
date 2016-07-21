using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using Blipkart.Service;
using Blipkart.ViewModel;
using Blipkart.Core;

namespace Blipkart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IProductService productService,
            ILogger<HomeController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");

            return View(_productService.GetItems());
        }

        public IActionResult Show(long Id){
            var p = _productService.GetById(Id);

            return View(new LineItem(){ProductId = p.ProductId, ProductName = p.ProductName, Price = p.Price});
        }
    }
}
