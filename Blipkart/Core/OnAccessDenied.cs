using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blipkart.Core.Security
{
    public static class OnAccessDenied
    {
        public static Task RedirectOnAccessDenied(CookieRedirectContext context)
        {
            return context.HttpContext.Authentication.SignOutAsync("BlipkartCookieMiddlewareInstance");
        }
    }
}
