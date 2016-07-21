using Microsoft.AspNetCore.Mvc;

using Blipkart.Service;
using Blipkart.ViewModel;

namespace Blipkart.Core.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke(int numberOfItems)
        {
            return View(_cartService.GetCart());
        }
    }
}
