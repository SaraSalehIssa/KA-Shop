using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace KASHop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        [Required]
        [Range(0.01, 100000)]
        public decimal Price { get; set; }
        [Range(0, 5)]
        public int Rate { get; set; }
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        [ValidateNever]
        public string Image { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
