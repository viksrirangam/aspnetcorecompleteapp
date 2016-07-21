using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blipkart.Core.Security
{
    public class IdentityHelper : IIdentityHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal CreatePrincipal(string Name, string Role, Dictionary<string, string> Claims = null)
        {
            var identity = new ClaimsIdentity("BlipkartCookieMiddlewareInstance", Name, Role);

            identity.AddClaim(new Claim("UserName", Name));
            if(Claims != null){
                foreach(var key in Claims.Keys){
                    identity.AddClaim(new Claim(key, Claims[key]));
                }
            }

            return new ClaimsPrincipal(identity);
        }

        public ClaimsPrincipal GetCurrentPrincipal(){
            return _httpContextAccessor.HttpContext.User as ClaimsPrincipal;
        }
    }
}
