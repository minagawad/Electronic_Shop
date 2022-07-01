using Electronic_Shop.Admin.Dto.Product;
using Electronic_Shop.Model;
using System;
using System.Threading.Tasks;

namespace Electronic_Shop.Admin.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;
        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> AddProduct(ProductDto productDto)
        {
            var product = new Entities.Product()
            {
                Id = Guid.NewGuid(),
                NameAr = productDto.NameAr,
                NameEn = productDto.NameEn,
                DescriptionAr = productDto.DescriptionAr,
                DescriptionEn = productDto.DescriptionEn,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId,

            };

            await this.context.Products.AddAsync(product);
            await this.context.SaveChangesAsync();

            return product.Id;
        }
    }
}
