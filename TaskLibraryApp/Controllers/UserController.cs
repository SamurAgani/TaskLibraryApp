using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskLibraryApp.Service;

namespace TaskLibraryApp.Controllers
{
    public class UserController : Controller
    {
        public IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            string userMail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = _userService.GetByEmail(userMail);
            return View(user);
        }
        public IActionResult CheckOutHistory()
        {
            string userMail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userHistory = _userService.GetUserHistory(userMail);
            return View(userHistory);
        }
    }
}
