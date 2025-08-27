using PizzaStore.Models.DTOs;

namespace PizzaStore.Services
{
    public interface IToppingService
    {
        Task<IEnumerable<ToppingDto>> GetAllToppingsAsync();
        Task<ToppingDto?> GetToppingByIdAsync(int id);
        Task<ToppingDto> CreateToppingAsync(CreateToppingDto createToppingDto);
        Task<ToppingDto?> UpdateToppingAsync(int id, UpdateToppingDto updateToppingDto);
        Task<bool> DeleteToppingAsync(int id);
        Task<bool> ToppingExistsAsync(string name, int? excludeId = null);
    }
}