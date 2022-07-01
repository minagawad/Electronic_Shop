using Electronic_Shop.Buyers.Models;
using Electronic_Shop.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Electronic_Shop.Buyers.Services
{
    public class OrderService : IOrderservice
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }



        //public async Task<bool> AddOrder(OrderModel orderModel)
        //{
        //   var user= await this._userManager.FindByIdAsync(orderModel.UserId);
        //    if (user is null)
        //        throw new System.Exception("InValidUser");
        //    else
        //    {

        //    }
        //}
    }
}
