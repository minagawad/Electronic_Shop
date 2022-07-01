using System;
using System.Collections.Generic;

namespace Electronic_Shop.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public ICollection<ProductOrder> ProductOrders { get; set; }

    }
}
