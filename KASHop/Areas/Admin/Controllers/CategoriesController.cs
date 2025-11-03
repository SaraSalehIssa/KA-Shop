using KASHop.Data;
using KASHop.Models;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            return View("Index", categories);
        }
        public IActionResult Create()
        {
            return View("Create", new Category());
        }

        [ValidateAntiForgeryToken]
        public IActionResult Store(Category request)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", request);
            }
            context.Categories.Add(request);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var category = context.Categories.Find(id);
            return View("Edit", category);
        }

        public IActionResult Update(Category request)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", request);
            }
            var category = context.Categories.Find(request.Id);
            category.Name = request.Name;
            category.Description = request.Description;
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
