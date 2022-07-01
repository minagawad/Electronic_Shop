using Electronic_Shop.Model;
using System.Threading.Tasks;

namespace Electronic_Shop.Services
{
    public interface IAuthService
    {

        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRolesAsync(AddRoleModel model);


    }
}
