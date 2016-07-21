using System;
using System.Collections.Generic;
using System.Linq;

using Blipkart.Model;

namespace Blipkart.Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Customer GetById(long id);
    }
}
