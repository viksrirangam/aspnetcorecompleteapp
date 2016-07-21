using System;
using System.Collections.Generic;
using System.Linq;

using Blipkart.Model;

namespace Blipkart.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetById(long id);
    }
}
