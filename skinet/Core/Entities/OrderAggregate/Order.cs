using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, Address shipToAddress,
            DelivaryMethod delivaryMethod, decimal subtotal)
        {
            Id = Guid.NewGuid().ToString();
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DelivaryMethod = delivaryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }  
        public DateTime OrdeDate { get; set; } = DateTime.Now;
        public Address ShipToAddress { get; set; }
        public DelivaryMethod DelivaryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DelivaryMethod.Price;
        }
    }
}
