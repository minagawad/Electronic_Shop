using Electronic_Shop.Buyers.Models;
using Electronic_Shop.Entities;
using Electronic_Shop.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronic_Shop.Buyers.Services
{
    public class OrderService : IOrderservice
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OrderService(UserManager<ApplicationUser> userManager,
                                ApplicationDbContext context )
        {
            _userManager = userManager;
            _context = context;
        }



        public async Task<int> AddOrder(OrderModel orderModel)
        {
            var user = await this._userManager.FindByIdAsync(orderModel.UserId);
            if (user is null)
                throw new System.Exception("InValidUser");
            else
            {
                Order order = new Order()
                {
                    UserId = user.Id,

                    TotalPrice = orderModel.Items.Sum(x => x.LastPrice)
                    
                };
                this._context.Orders.Add(order);
                List<ProductOrder> productOrder = new List<ProductOrder>();

                foreach (var item in orderModel.Items)
                {

                    var d = new ProductOrder()
                    {
                        ProductId = item.ItemId,
                        MainPrice = item.Price,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        PriceAfterDiscount = item.LastPrice,
                        OrederDate = System.DateTime.UtcNow
                    };
                }
                this._context.ProductOrders.AddRange(productOrder);
                this._context.SaveChanges();
                return order.Id;

            }
        }
    }
}
