using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace KASHop.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(10)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ValidateNever]
        List<Product> Products { get; set; } = new List<Product>();
    }
}
