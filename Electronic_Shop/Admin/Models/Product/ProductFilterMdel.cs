using Electronic_Shop.Common;

namespace Electronic_Shop.Admin.Models.Product
{
    public class ProductFilterMdel :PagingParams
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int? CategoryId { get; set; }

    }
}
