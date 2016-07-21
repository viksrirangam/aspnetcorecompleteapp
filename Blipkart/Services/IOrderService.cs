using System;
using System.Collections.Generic;
using System.Linq;

using Blipkart.Model;
using Blipkart.ViewModel;

namespace Blipkart.Service
{
    public interface IOrderService
    {
        IEnumerable<OrderInfo> GetByCustomerId(long Id);
        void CreateOrder(Cart Cart);
    }
}
