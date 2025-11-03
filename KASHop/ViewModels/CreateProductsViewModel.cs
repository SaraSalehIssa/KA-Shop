using KASHop.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace KASHOP.ViewModels
{
    public class CreateProductsViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
