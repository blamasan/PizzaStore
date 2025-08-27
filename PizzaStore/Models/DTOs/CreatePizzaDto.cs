using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models.DTOs
{
    public class CreatePizzaDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0.01, 999.99)]
        public decimal BasePrice { get; set; }

        public List<int> ToppingIds { get; set; } = new();
    }
}