using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Blipkart.Service;
using Blipkart.ViewModel;
using Blipkart.Core;
using Blipkart.Core.Security;

namespace Blipkart.Controllers
{
    public class CartController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly ILogger<HomeController> _logger;
        private readonly IIdentityHelper _identityHelper;

        public CartController(
            IOrderService orderService,
            ICustomerService customerService,
            ICartService cartService,
            IIdentityHelper identityHelper,
            ILogger<HomeController> logger)
        {
            _orderService = orderService;
            _customerService = customerService;
            _cartService = cartService;
            _identityHelper = identityHelper;
            _logger = logger;
        }

        public IActionResult Cart(){
            Cart _cart = _cartService.GetCart();

            UpdateCartCustomerInfo(_cart);

            return View(_cart);
        }

        public IActionResult Add(LineItem item){
            Cart _cart = _cartService.GetCart();
            _cart.LineItems.Add(item);

            _cartService.UpdateCart(_cart.CartId, _cart);

            return RedirectToAction("Cart");
        }

        [Authorize]
        public IActionResult Buy()
        {
            Cart _cart = _cartService.GetCart();

            UpdateCartCustomerInfo(_cart);

            if(!_cart.IsEmpty){
                _orderService.CreateOrder(_cart);
            }

            _cart.LineItems.Clear();
            _cartService.UpdateCart(_cart.CartId, _cart);

            return RedirectToAction("Orders");
        }

        [Authorize]
        public IActionResult Orders()
        {
            var principal = _identityHelper.GetCurrentPrincipal();
            var customerId = Int64.Parse(principal.FindFirst("Id").Value);

            var orders = _orderService.GetByCustomerId(customerId);
            //string output = JsonConvert.SerializeObject(orders);
            //_logger.LogInformation(LoggingEvents.LIST_ITEMS, output);

            return View(orders);
        }

        [NonAction]
        private void UpdateCartCustomerInfo(Cart _cart)
        {
            var principal = _identityHelper.GetCurrentPrincipal();

            if(principal != null && principal.Identity.IsAuthenticated)
            {
                _cart.CustomerId = Int64.Parse(principal.FindFirst("Id").Value);
                _cart.CustomerName = principal.FindFirst("UserName").Value;

                _cartService.UpdateCart(_cart.CartId, _cart);
            }
        }
    }
}
