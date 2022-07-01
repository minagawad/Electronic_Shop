using System;
using System.Collections.Generic;

namespace Electronic_Shop.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn {  get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
        public ICollection<DiscountConfiguration>  discountConfigurations { get; set; }





    }
}
