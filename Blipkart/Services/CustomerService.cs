using System;
using System.Collections.Generic;
using System.Linq;

using Blipkart.Model;
using Blipkart.Repository;

namespace Blipkart.Service
{
    public class CustomerService : EntityService<Customer>, IEntityService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
            : base(unitOfWork, customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        long ICustomerService.GetIdByName(string Name) {
            var customers = _customerRepository.FindBy(c => c.Name == Name).ToList();

            return customers.Count > 0 ? customers[0].Id : -1L;
        }
    }
}
