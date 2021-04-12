using Core.Entities.Identity;
using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OrderToReturnDto
    {
        public string Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrdeDate { get; set; } 
        public Core.Entities.OrderAggregate.Address ShipToAddress { get; set; }
        public string DelivaryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}
