using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models.DTOs
{
    public class CreateToppingDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;        
        
        [Range(0.01, 999.99)]
        public decimal Price { get; set; } 
    }
}
