using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Globalization;

namespace Electronic_Shop.Common
{
    public class IdentityUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? CurrentUserID
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext.User.FindFirst("sub");
                if (claim != null)
                    return Guid.Parse(claim.Value);
                return (Guid?)null;
            }
        }
        public string UserName
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext.User.FindFirst("given_name");
                if (claim != null)
                    return claim.Value;
                return string.Empty;
            }
        }
        public string RemoteIpAddress
        {
            get
            {
                var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                if (ip != null)
                    return ip;
                return "IP not exist";
            }
        }

        public string LangKey
        {
            get
            {
                var isExist = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("langKey", out StringValues value);
                if (isExist)
                    return value.ToString();

                return Constants.DefaultLang;
            }
        }
        public CultureInfo CultureInfo
        {
            get
            {
                var isExist = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("langKey", out StringValues value);
                if (isExist)
                    return CultureInfo.GetCultureInfo(value == Constants.English ? Constants.CultureInfo_EN : Constants.CultureInfo_AR);

                return CultureInfo.GetCultureInfo(Constants.CultureInfo_AR);
            }
        }

    }
}
