using System;
using System.Collections.Generic;
using System.Linq;

using Blipkart.Model;
using Blipkart.ViewModel;

namespace Blipkart.Service
{
    public interface IProductService
    {
        Item GetById(long Id);
        IEnumerable<Item> GetItems();
    }
}
