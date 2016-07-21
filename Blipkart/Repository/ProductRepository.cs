using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Blipkart.Model;

namespace Blipkart.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context)
            : base(context)
        {

        }

        public override IEnumerable<Product> GetAll()
        {
            return _entities.Set<Product>().AsEnumerable();
        }

        public Product GetById(long id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
