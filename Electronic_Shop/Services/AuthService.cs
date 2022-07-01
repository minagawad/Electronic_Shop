
namespace Electronic_Shop.Services
{
    using Electronic_Shop.Entities;
    using Electronic_Shop.Helpers;
    using Electronic_Shop.Model;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(
            UserManager<ApplicationUser> userManager,
             IOptions<JWT> jwt,
             RoleManager<IdentityRole> roleManager

             )
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;

        }

        public async Task<string> AddRolesAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))

            {
                return "Invalid userId or Role";

            }

            if (await _userManager.IsInRoleAsync(user, model.Role))
            {

                return "user already assign to role";
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "some thing wrong";


        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);


            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {

                authModel.Message = "Email or passwor is incorrect";
                return authModel;

            }

            var jwtSecurityToken = await CreateJwtToken(user);

            var roles = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.UserName = user.UserName;
            authModel.Roles = roles.ToList();

            return authModel;


        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {

            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "Email is already exist" };
            }

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
            {
                return new AuthModel { Message = "UserName is already exist" };
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber=model.PhoneNumber,
                Address = model.Address,
               


            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {

                var errors = string.Empty;
                foreach (var erroe in result.Errors)
                {
                    errors += $"{erroe.Description},";
                }
                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName

            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                userClaims.Add(new Claim("rols", role));

            }
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
           .Union(userClaims)
           .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;

        }



        //public async Task<bool> RevokeTokenAsync(string token)
        //{
        //    var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

        //    if (user == null)
        //        return false;

        //    var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

        //    if (!refreshToken.IsActive)
        //        return false;

        //    refreshToken.RevokedOn = DateTime.UtcNow;

        //    await _userManager.UpdateAsync(user);

        //    return true;
        //}
    }
}
