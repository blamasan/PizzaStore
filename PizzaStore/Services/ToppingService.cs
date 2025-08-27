using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;
using PizzaStore.Models.DTOs;

namespace PizzaStore.Services
{
    public class ToppingService : IToppingService
    {
        private readonly PizzaStoreContext _context;

        public ToppingService(PizzaStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToppingDto>> GetAllToppingsAsync()
        {
            return await _context.Toppings
                .Select(t => new ToppingDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price
                })
                .ToListAsync();
        }

        public async Task<ToppingDto?> GetToppingByIdAsync(int id)
        {
            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null) return null;

            return new ToppingDto
            {
                Id = topping.Id,
                Name = topping.Name,
                Price = topping.Price
            };
        }

        public async Task<ToppingDto> CreateToppingAsync(CreateToppingDto createToppingDto)
        {
            var topping = new Topping
            {
                Name = createToppingDto.Name.Trim(),
                Price = createToppingDto.Price
            };

            _context.Toppings.Add(topping);
            await _context.SaveChangesAsync();

            return new ToppingDto
            {
                Id = topping.Id,
                Name = topping.Name,
                Price = topping.Price
            };
        }

        public async Task<ToppingDto?> UpdateToppingAsync(int id, UpdateToppingDto updateToppingDto)
        {
            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null) return null;

            topping.Name = updateToppingDto.Name.Trim();
            topping.Price = updateToppingDto.Price;

            await _context.SaveChangesAsync();

            return new ToppingDto
            {
                Id = topping.Id,
                Name = topping.Name,
                Price = topping.Price
            };
        }

        public async Task<bool> DeleteToppingAsync(int id)
        {
            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null) return false;

            _context.Toppings.Remove(topping);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToppingExistsAsync(string name, int? excludeId = null)
        {
            var query = _context.Toppings.Where(t => t.Name.ToLower() == name.Trim().ToLower());

            if (excludeId.HasValue)
            {
                query = query.Where(t => t.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}