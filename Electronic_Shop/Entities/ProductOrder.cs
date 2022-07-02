using System;

namespace Electronic_Shop.Entities
{
    public class ProductOrder
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime OrederDate { get; set; }
        public decimal MainPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
