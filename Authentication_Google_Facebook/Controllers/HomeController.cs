using Authentication_Google_Facebook.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace Authentication_Google_Facebook.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("provider/{provider}")]
        public IActionResult GetProvider(string provider)
        {
            var redirectUrl = Url.RouteUrl("ExternalLogin", Request.Scheme);
            var peroperties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(peroperties, provider);
        }

        [Route("ExternalLogin", Name = "ExternalLogin")]
        public IActionResult ExternalLogin()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,"USerId") ,
                new Claim(ClaimTypes.Name,"Arash")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); 
            var principal = new ClaimsPrincipal(identity); 
            var properties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            HttpContext.SignInAsync(principal, properties);
            return RedirectToAction(nameof(Privacy));
        }
    }
}
