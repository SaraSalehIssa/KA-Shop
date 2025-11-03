using KASHop.Data;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.Areas.User.Controllers
{
    [Area("User")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }
    }
}
