using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskLibraryApp.CacheManager;
using TaskLibraryApp.Models;
using TaskLibraryApp.Service;

namespace TaskLibraryApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICacheManager _cacheManager;
        public IBookService _bookService;

        public HomeController(ILogger<HomeController> logger, ICacheManager cacheManager, IBookService bookService) 
        {
            _cacheManager = cacheManager;
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks(true).ToList();
            return View(books);
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

        public IActionResult Detail(int id)
        {
            var book = _bookService.GetById(id, true);
            return View(book);
        }
        public IActionResult Book(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            _bookService.BookTheBook(new BookTheBookVM() { BookId = id, UserId = int.Parse(userId) });
            return View();
        }
    }
}