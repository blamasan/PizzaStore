using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class Topping
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
    }
}