using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        public readonly IGenericRepository<Order> _orderRepo;
        public readonly IGenericRepository<DelivaryMethod> _dmRepo;
        public readonly IGenericRepository<Product> _productRepo;
        public readonly IBasketRepository _basketRepo;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DelivaryMethod> dmRepo, 
            IGenericRepository<Product> productRepo, IBasketRepository basketRepo)
        {
            _orderRepo = orderRepo;
            _dmRepo = dmRepo;
            _productRepo = productRepo;
            _basketRepo = basketRepo;
        }

       

        public async Task<Order> CreateOrderAsync(string buyerEmail, string deliveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var productItem = await _productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            var delivaryMethod = await _dmRepo.GetByIdAsync(deliveryMethodId);
            var subtotal = items.Sum(item => item.Price * item.Quality);
            var order = new Order(items, buyerEmail, shippingAddress, delivaryMethod, subtotal);
            return order;
        }

   
        public Task<IReadOnlyList<DelivaryMethod>> GetDelivaryMethodsAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(string id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
