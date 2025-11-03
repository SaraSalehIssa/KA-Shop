using KASHop.Data;
using KASHop.Models;
using KASHOP.ViewModels;
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
            //var products = context.Products.Join(context.Categories, p=>p.CategoryId, 
            //    c=>c.Id, (p, c)=> new
            //    {
            //         p.Id, p.Name, p.Description, p.Price, p.Image, categoryName = c.Name
            //    });

            var products = context.Products.Include(p => p.Category).ToList();
            var productsViewModel = new List<ProductsViewModel>();

            foreach (var product in products)
            {
                var viewModel = new ProductsViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/images/{product.Image}",
                    //CategoryName = product.categoryName
                    CategoryName = product.Category.Name
                };
                productsViewModel.Add(viewModel);
            }
            return View(productsViewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View(new Product());
        }

        //[ValidateAntiForgeryToken]
        public IActionResult Store(Product request, IFormFile file)
        {
            ViewBag.Categories = context.Categories.ToList();
            ModelState.Remove("File");

            if (!ModelState.IsValid)
            {
                return View("Create", request);
            }

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("Image", "Please upload an Image!");
                return View("Create", request);
            }

            //File Extension
            var allowedExtensions = new[] {".jpg", ".webp", ".png"};
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("Image", "Error in Image Extension!");
                return View("Create", request);
            }

            //File Size: Larger than 2 MB
            if(file.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Image Size must be less than 2 MB!");
                return View("Create", request);
            }

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
