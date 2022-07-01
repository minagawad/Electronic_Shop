using Electronic_Shop.Admin.Dto.Product;
using Electronic_Shop.Admin.Models.Product;
using Electronic_Shop.Common;
using Electronic_Shop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
            if (productDto == null)
            {

                throw new ArgumentNullException(nameof(productDto));

            }
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

        public async Task<ListModel<ProductDto>> GetAll(ProductFilterMdel productFilter)
        {
            if (productFilter is null)
                throw new ArgumentNullException(nameof(productFilter));

            var query = this.context.Products.
                                     Include(p => p.Category).
                                     Include(p=>p.discountConfigurations)
                                     .Where(p => (p.CategoryId == productFilter.CategoryId || !productFilter.CategoryId.HasValue) &&
                                     (p.NameAr.Contains(productFilter.NameAr) || string.IsNullOrEmpty(productFilter.NameAr)) && (p.NameEn.Contains(productFilter.NameEn) || string.IsNullOrEmpty(productFilter.NameEn)));

            var items = await query.Skip(productFilter.Skip).Take(productFilter.Take).Select(p =>
                new ProductDto()
                {
                    Id = p.Id,
                    NameAr = p.NameAr,
                    NameEn = p.NameEn,
                    DescriptionAr = p.DescriptionAr,
                    DescriptionEn = p.DescriptionEn,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    Quantity = p.Quantity,
                    

                }).ToListAsync();
            var response = new ListModel<ProductDto>
            {
                Items = items,
                TotalCount = await query.CountAsync(),

            };
            return response;


        }

        public async Task<ProductDto> GetById(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId), "Value cannot be null or empty string.");

            }
            var product = await this.context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product != null)
            {
                var res = new ProductDto()
                {
                    Id = productId,
                    NameAr = product.NameAr,
                    NameEn = product.NameEn,
                    DescriptionAr = product.DescriptionAr,
                    DescriptionEn = product.DescriptionEn,
                    Price = product.Price,
                    Quantity = product.Quantity,

                };
                return res;

            }
            else
                return null;

        }

        public async Task DeletProduct(Guid productId)
        {
            if (productId ==Guid.Empty)
                throw new ArgumentNullException(nameof(productId));

            var product = this.context.Products.FirstOrDefault(p => p.Id == productId);
            if(product is null)
               throw new  Exception("product not found");

            else
            {
                this.context.Products.Remove(product);
              await this.context.SaveChangesAsync();

            }


        }
    }
}
