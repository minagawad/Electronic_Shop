using Electronic_Shop.Admin.Dto.Product;
using Electronic_Shop.Admin.Models.Product;
using Electronic_Shop.Buyers.Dto;
using Electronic_Shop.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronic_Shop.Admin.Service.Product
{
    public interface IProductService
    {
        Task<Guid> AddProduct(ProductDto product);
        Task<ListModel<ProductDto>> GetAll(ProductFilterMdel productFilter);
        Task<ProductDto> GetById(Guid productId);
        Task DeletProduct(Guid productId);
        Task AddDiscount(Guid id, List<DiscountCounfigurationModel> discountCounfigurationModels);
        Task<ListModel<PortalProductDto>> RetrieveProductForPortal(ProductFilterMdel productFilter);

    }
}
