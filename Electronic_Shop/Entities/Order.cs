using System;
using System.Collections.Generic;

namespace Electronic_Shop.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }

    }
}
