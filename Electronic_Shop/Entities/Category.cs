
namespace Electronic_Shop.Entities
{
    using System.Collections.Generic;
        
    public class Category
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
