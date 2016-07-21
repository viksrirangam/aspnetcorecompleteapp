using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Blipkart.Model;

namespace Blipkart.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context)
            : base(context)
        {

        }

        public override IEnumerable<Customer> GetAll()
        {
            return _entities.Set<Customer>().AsEnumerable();
        }

        public Customer GetById(long id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
