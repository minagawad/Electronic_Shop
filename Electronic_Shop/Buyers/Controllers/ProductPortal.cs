using Electronic_Shop.Admin.Dto.Product;
using Electronic_Shop.Admin.Models.Product;
using Electronic_Shop.Admin.Service.Product;
using Electronic_Shop.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronic_Shop.Buyers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPortal : ControllerBase
    {
        private readonly IProductService productService;

        public ProductPortal(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        [Route("Filter")]
        [ProducesResponseType(typeof(MessageResponse<ListModel<ProductDto>>), 200)]
        [ProducesResponseType(typeof(List<string>), 400)]


        public async Task<IActionResult> RetriveProducts([FromBody] ProductFilterMdel productFilter)
        {
            var response = new MessageResponse<ListModel<ProductDto>>();

            try
            {
                response.RequestedObject = await this.productService.GetAll(productFilter);
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessages = new List<string>() { ex.Message };
                response.IsSuccess = false;
                return BadRequest(response);
            }

        }
    }
}
