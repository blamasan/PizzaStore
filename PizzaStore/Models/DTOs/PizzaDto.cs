namespace PizzaStore.Models.DTOs
{
    public class PizzaDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ToppingDto> Toppings { get; set; } = new();
    }
}