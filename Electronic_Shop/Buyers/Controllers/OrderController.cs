using Electronic_Shop.Buyers.Models;
using Electronic_Shop.Buyers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Electronic_Shop.Buyers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderservice _orderService;
        public OrderController(IOrderservice orderservice)
        {
            _orderService = orderservice;

        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> CreatOrder(OrderModel orderModel)
        {
            try
            {
                var id = await this._orderService.AddOrder(orderModel);
                return Ok(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
