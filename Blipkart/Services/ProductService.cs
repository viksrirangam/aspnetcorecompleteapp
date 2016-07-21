using System;
using System.Collections.Generic;
using System.Linq;

using Blipkart.Model;
using Blipkart.Repository;
using Blipkart.ViewModel;

namespace Blipkart.Service
{
    public class ProductService : EntityService<Product>, IEntityService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
            : base(unitOfWork, productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        IEnumerable<Item> IProductService.GetItems() {
            return _productRepository
                .GetAll()
                .Select(p => new Item()
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                        Price = p.UnitPrice
                    }
               );
        }

        Item IProductService.GetById(long Id) {
            var product = _productRepository.GetById(Id);

            return new Item()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.UnitPrice
            };
        }
    }
}
