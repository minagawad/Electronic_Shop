namespace Electronic_Shop.Controllers
{
    using Electronic_Shop.Model;
    using Electronic_Shop.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;



    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync ([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.AddRolesAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        //[HttpPost("revokeToken")]
        //public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
        //{
        //    var token = model.Token ?? Request.Cookies["refreshToken"];

        //    if (string.IsNullOrEmpty(token))
        //        return BadRequest("Token is required!");

        //    var result = await _authService.RevokeTokenAsync(token);

        //    if (!result)
        //        return BadRequest("Token is invalid!");

        //    return Ok();
        //}
    }
}
