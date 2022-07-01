using System;

namespace Electronic_Shop.Entities
{
    public class DiscountConfiguration
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int ItemCount { get; set; }
    }
}
