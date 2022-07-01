using Electronic_Shop.Admin.Dto.Product;
using System;
using System.Threading.Tasks;

namespace Electronic_Shop.Admin.Service.Product
{
    public interface IProductService
    {
        Task<Guid> AddProduct(ProductDto product);

    }
}
