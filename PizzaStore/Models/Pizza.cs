using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public decimal BasePrice { get; set; }


        public ICollection<Topping> Toppings { get; set; } = new List<Topping>();
    }
}