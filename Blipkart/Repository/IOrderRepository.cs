using System;
using System.Collections.Generic;
using System.Linq;

using Blipkart.Model;

namespace Blipkart.Repository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Order GetById(long id);
        IEnumerable<Order> GetByCustomerId(long Id);
    }
}
