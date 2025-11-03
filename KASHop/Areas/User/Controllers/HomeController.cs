using System.Diagnostics;
using KASHop.Data;
using KASHop.Models;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Model
            //var categories = _context.Categories.ToList();
            //return View("Index", categories);

            //ViewData
            //ViewData["categories"] = _context.Categories.ToList();
            //return View("Index");

            //ViewBag
            ViewBag.categories = _context.Categories.ToList();
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
    }
}
