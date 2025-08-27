using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;
using PizzaStore.Models.DTOs;

namespace PizzaStore.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly PizzaStoreContext _context;

        public PizzaService(PizzaStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PizzaDto>> GetAllPizzasAsync()
        {
            return await _context.Pizzas
                .Include(p => p.Toppings)
                .Select(p => new PizzaDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    BasePrice = p.BasePrice,
                    TotalPrice = p.BasePrice + p.Toppings.Sum(t => t.Price),
                    Toppings = p.Toppings.Select(t => new ToppingDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Price = t.Price
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<PizzaDto?> GetPizzaByIdAsync(int id)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.Toppings)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pizza == null) return null;

            return new PizzaDto
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Description = pizza.Description,
                BasePrice = pizza.BasePrice,
                TotalPrice = pizza.BasePrice + pizza.Toppings.Sum(t => t.Price),
                Toppings = pizza.Toppings.Select(t => new ToppingDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price
                }).ToList()
            };
        }

        public async Task<PizzaDto> CreatePizzaAsync(CreatePizzaDto createPizzaDto)
        {
            var toppings = await _context.Toppings
                .Where(t => createPizzaDto.ToppingIds.Contains(t.Id))
                .ToListAsync();

            var pizza = new Pizza
            {
                Name = createPizzaDto.Name.Trim(),
                Description = createPizzaDto.Description?.Trim(),
                BasePrice = createPizzaDto.BasePrice,
                Toppings = toppings
            };

            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();

            return new PizzaDto
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Description = pizza.Description,
                BasePrice = pizza.BasePrice,
                TotalPrice = pizza.BasePrice + pizza.Toppings.Sum(t => t.Price),
                Toppings = pizza.Toppings.Select(t => new ToppingDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price
                }).ToList()
            };
        }

        public async Task<PizzaDto?> UpdatePizzaAsync(int id, UpdatePizzaDto updatePizzaDto)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.Toppings)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pizza == null) return null;

            var toppings = await _context.Toppings
                .Where(t => updatePizzaDto.ToppingIds.Contains(t.Id))
                .ToListAsync();

            pizza.Name = updatePizzaDto.Name.Trim();
            pizza.Description = updatePizzaDto.Description?.Trim();
            pizza.BasePrice = updatePizzaDto.BasePrice;
            pizza.Toppings.Clear();
            pizza.Toppings = toppings;

            await _context.SaveChangesAsync();

            return new PizzaDto
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Description = pizza.Description,
                BasePrice = pizza.BasePrice,
                TotalPrice = pizza.BasePrice + pizza.Toppings.Sum(t => t.Price),
                Toppings = pizza.Toppings.Select(t => new ToppingDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price
                }).ToList()
            };
        }

        public async Task<bool> DeletePizzaAsync(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null) return false;

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PizzaExistsAsync(string name, int? excludeId = null)
        {
            var query = _context.Pizzas.Where(p => p.Name.ToLower() == name.Trim().ToLower());

            if (excludeId.HasValue)
            {
                query = query.Where(p => p.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}