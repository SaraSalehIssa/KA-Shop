using KASHop.Data;
using KASHop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View(new Product());
        }

        public IActionResult Store(Product request, IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                //var fileName = DateTime.Now + file.FileName;
                var fileName = Guid.NewGuid().ToString(); //sara
                fileName += Path.GetExtension(file.FileName); //sara.png

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                request.Image = fileName;
                context.Products.Add(request);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = context.Categories.ToList();
            return View("Create", request);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = context.Categories.ToList();
            var product = context.Products.Find(id);
            if (product != null)
            {
                return View(product);

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(Product request, IFormFile? file)
        {
            //var product = context.Products.AsNoTracking().FirstOrDefault(c => c.Id == request.Id);
            var product = context.Products.Find(request.Id);
            product.Name = request.Name;
            product.Description = request.Description;
            product.Quantity = request.Quantity;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;

            if (file != null && file.Length > 0)
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", product.Image);
                System.IO.File.Delete(oldFilePath);

                var fileName = Guid.NewGuid().ToString(); //sara
                fileName += Path.GetExtension(file.FileName); //sara.png

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.Image = fileName;
            }
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var product = context.Products.Find(id);

            if (product != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", product.Image);
                System.IO.File.Delete(filePath);
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
