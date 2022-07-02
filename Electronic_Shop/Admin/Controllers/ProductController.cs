using Electronic_Shop.Admin.Dto.Product;
using Electronic_Shop.Admin.Models.Product;
using Electronic_Shop.Admin.Service.Product;
using Electronic_Shop.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronic_Shop.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;

        }
        [HttpPost]
        [Route("Creat")]
        [ProducesResponseType(typeof(MessageResponse<ProductDto>), 200)]
        [ProducesResponseType(typeof(List<string>), 400)]

        public async Task<IActionResult> Add(ProductDto product)
        {
            var response = new MessageResponse<ProductDto>();

            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.ErrorMessages = this.GetErrors();
            }
            else
            {
                try
                {

                    var res = await this.productService.AddProduct(product);
                    response.IsSuccess = true;
                    response.RequestedObject = product;

                    return Ok(response);

                }
                catch (Exception ex)

                {
                    response.IsSuccess = false;
                    response.ErrorMessages = new System.Collections.Generic.List<string> { ex.Message };
                    return BadRequest(response);

                }
            }

            return BadRequest(response);
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

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var res = await this.productService.GetById(id);
            if (res == null)
                return NotFound();
            else
                return Ok(res);

        }
        [HttpDelete]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(List<string>), 400)]
        [Route("id/{id}")]

        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            try
            {
                await this.productService.DeletProduct(id);
               
                    return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

        }


        [HttpPost]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(List<string>), 400)]
        [Route("id/{id}/discountCounfig")]
        public async Task<IActionResult> AddDiscount([FromRoute] Guid id, [FromBody]List<DiscountCounfigurationModel> discountCounfigurationModels)
        {
       var product=   await  this.productService.GetById(id);
            if (product == null)
                throw new  Exception("Product With this Id not found");

            else
            {
                try
                {
                   await this.productService.AddDiscount(id, discountCounfigurationModels);
                    return NoContent();
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);                }


            }
        }

       


    }
}
