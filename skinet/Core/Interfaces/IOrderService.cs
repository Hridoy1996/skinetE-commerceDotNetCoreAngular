using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string deliveryMethod, string basketId,
            Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(string id, string buyerEmail);

        Task<IReadOnlyList<DelivaryMethod>> GetDelivaryMethodsAsync(string buyerEmail);
    }
}
