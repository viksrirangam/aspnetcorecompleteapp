using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Blipkart.Model;

namespace Blipkart.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context)
            : base(context)
        {

        }

        public override IEnumerable<Order> GetAll()
        {
            return _entities.Set<Order>().AsEnumerable();
        }

        public IEnumerable<Order> GetByCustomerId(long Id)
        {
            return _entities.Set<Order>()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(c => c.CustomerId == Id)
                .OrderByDescending(o => o.Id)
                .Take(10);
        }

        public Order GetById(long id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
