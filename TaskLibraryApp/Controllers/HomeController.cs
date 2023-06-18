using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var books = _bookService.GetAllBooks(false).ToList();
            _bookService.CheckBookStatuses();
            return View(books);
        }

        public IActionResult MyBooks()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var books = _bookService.GetMyAllBooks(int.Parse(userId));
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
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var book = _bookService.GetBookDetails(id, int.Parse(userId));
            return View(book);
        }

        public IActionResult Return(int id)
        {
            var book = _bookService.GiveBookBack(id);
            return RedirectToAction("Index");
        }
        public IActionResult Book(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            _bookService.BookTheBook(new BookTheBookVM() { BookId = id, UserId = int.Parse(userId) });
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(int id)
        {
            var categories = _cacheManager.GetCategories();
            var book = _bookService.GetById(id, true);
            var updateBook = new CreateUpdateBookVM()
            {
                Author = book.Author,
                CategoryId = book.CategoryId,
                ShortDescription = book.ShortDescription,
                Content = book.Content,
                Title = book.Title
            };
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View(updateBook);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateUpdateBookVM createBookVM)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            createBookVM.UserId = int.Parse(userId);
            var categories = _cacheManager.GetCategories();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            _bookService.CreateOrUpdateBook(createBookVM);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            var categories = _cacheManager.GetCategories();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateBookVM createBookVM)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            createBookVM.UserId = int.Parse(userId);
            var categories = _cacheManager.GetCategories();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            _bookService.CreateOrUpdateBook(createBookVM);
            return RedirectToAction("Index");
        }
    }
}