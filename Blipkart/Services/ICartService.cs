using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blipkart.ViewModel;

namespace Blipkart.Service
{
    public interface ICartService {
        Cart GetCart();
        void UpdateCart(string id, Cart cart);
        void RemoveCart(string id);
    }
}
