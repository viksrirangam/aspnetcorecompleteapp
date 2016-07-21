using System;
using System.Collections.Generic;
using System.Linq;

using Blipkart.Model;
using Blipkart.ViewModel;
using Blipkart.Repository;

namespace Blipkart.Service
{
    public class OrderService : EntityService<Order>, IEntityService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
            : base(unitOfWork, orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        IEnumerable<OrderInfo> IOrderService.GetByCustomerId(long Id)
        {
            var orders = _orderRepository.GetByCustomerId(Id);

            var _orders = new List<OrderInfo>();
            foreach(var order in orders){
                var _order = new OrderInfo()
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount
                };

                _order.LineItems = order.OrderItems
                    .Select(lineItem => new LineItem()
                            {
                                ProductId = lineItem.ProductId,
                                ProductName = lineItem.Product.Name,
                                Quantity = lineItem.Quantity,
                                Price = lineItem.Price
                            })
                    .ToList();

                _orders.Add(_order);
            }

            return _orders;
        }

        public override void Create(Order entity){
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.OrderDate = DateTime.Now;
            entity.TotalAmount = entity.OrderItems.Sum(p => p.Price);

            //base.Create(entity);
            _orderRepository.Add(entity);
            _unitOfWork.Commit();
        }

        void IOrderService.CreateOrder(Cart Cart){
            var _order = new Order(){
                CustomerId = Cart.CustomerId,
                OrderItems = new List<OrderItem>()
            };

            _order.OrderItems = Cart.LineItems.Select(
                lineItem => new OrderItem()
                {
                    ProductId = lineItem.ProductId,
                    Quantity = lineItem.Quantity,
                    Price = lineItem.Price
                })
                .ToList();

            Create(_order);
        }
    }
}
