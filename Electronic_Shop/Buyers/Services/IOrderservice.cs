using Electronic_Shop.Buyers.Models;
using System.Threading.Tasks;

namespace Electronic_Shop.Buyers.Services
{
    public interface IOrderservice
    {
        Task<int> AddOrder(OrderModel orderModel);
    }
}
