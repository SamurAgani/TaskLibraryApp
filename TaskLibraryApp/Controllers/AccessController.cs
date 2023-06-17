using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskLibraryApp.Models;
using TaskLibraryApp.Service;

namespace TaskLibraryApp.Controllers
{
    public class AccessController : Controller
    {
        public IUserService _userService;

        public AccessController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if(claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(VMLogin VMLogin)
        {
            if(ModelState.IsValid)
            {
                var user = _userService.CheckUser(VMLogin);
                if(user == null)
                {
                    ViewData["ValidateMessage"] = "Username or Password is not correct!";
                    return View();
                }

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, VMLogin.Email)
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = VMLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);
                return RedirectToAction("Index","Home");
            }

            ViewData["ValidateMessage"] = "Please fill the all required fields!";
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
