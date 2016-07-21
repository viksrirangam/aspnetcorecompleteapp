using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Blipkart.Core.Security
{
    public interface IIdentityHelper
    {
        ClaimsPrincipal CreatePrincipal(string Name, string Role, Dictionary<string, string> Claims = null);
        ClaimsPrincipal GetCurrentPrincipal();
    }
}
