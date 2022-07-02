using System;
using System.Collections.Generic;

namespace Electronic_Shop.Buyers.Dto
{
    public class PortalProductDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        public List<DiscountConfigDto>  DiscountConfigDtos { get; set; }
    }
}
