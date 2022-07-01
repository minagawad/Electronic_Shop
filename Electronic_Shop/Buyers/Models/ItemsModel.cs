using System;

namespace Electronic_Shop.Buyers.Models
{
    public class ItemsModel
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal LastPrice { get; set; }
    }
}
