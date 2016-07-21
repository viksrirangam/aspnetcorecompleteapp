using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;

using Blipkart.ViewModel;

namespace Blipkart.Service
{
    public class CartService : ICartService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string sessionId => _httpContextAccessor.HttpContext.Session.Id;

        public CartService(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor) {
            _memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
        }

        public Cart GetCart(){
            Cart _cart = null;

            if(!_memoryCache.TryGetValue(sessionId, out _cart))
            {
                _cart = new Cart(){CartId = sessionId, CustomerName = "Guest"};
                _memoryCache.Set(sessionId, _cart);
            }

            return _cart;
        }

        public void UpdateCart(string id, Cart cart)
        {
            _memoryCache.Set(id, cart);
        }

        public void RemoveCart(string id)
        {
            _memoryCache.Remove(id);
        }
    }
}
