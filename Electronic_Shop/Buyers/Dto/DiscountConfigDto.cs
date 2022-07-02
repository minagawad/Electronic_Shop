using System;

namespace Electronic_Shop.Buyers.Dto
{
    public class DiscountConfigDto
    {
        public int Id { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int ItemCount { get; set; }
    }
}
