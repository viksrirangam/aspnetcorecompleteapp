using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using Blipkart.Service;
using Blipkart.ViewModel;
using Blipkart.Core;
using Blipkart.Core.Security;

namespace Blipkart.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<HomeController> _logger;
        private readonly IIdentityHelper _identityHelper;

        public AccountController(
            ICustomerService customerService,
            IIdentityHelper identityHelper,
            ILogger<HomeController> logger)
        {
            _customerService = customerService;
            _logger = logger;
            _identityHelper = identityHelper;
        }

        public IActionResult Login([FromQuery]string ReturnUrl)
        {
            var loginInfo = new LoginInfo(){ReturnUrl = "/"};

            if(!string.IsNullOrEmpty(ReturnUrl)){
                loginInfo.ReturnUrl = ReturnUrl;
            }

            return View(loginInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInfo loginInfo)
        {
            if(ModelState.IsValid){
                var Id = _customerService.GetIdByName(loginInfo.UserName);

                if(Id > 0){
                    var Claims = new Dictionary<string, string>(){
                        ["Id"] = Id.ToString()
                    };
                    var principal = _identityHelper.CreatePrincipal(loginInfo.UserName, "", Claims);

                    await HttpContext.Authentication.SignInAsync("BlipkartCookieMiddlewareInstance", principal);
                }
            }else
            {
                return RedirectToAction("Forbidden", "Account");
            }

            var uri = loginInfo.ReturnUrl.Split(new char[]{'/'});
            return RedirectToAction(uri[2], uri[1]);
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.Authentication.SignOutAsync("BlipkartCookieMiddlewareInstance");

            return RedirectToAction("Index", "Home");
        }
    }
}
