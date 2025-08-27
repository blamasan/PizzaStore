using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models.DTOs;
using PizzaStore.Services;

namespace PizzaStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToppingsController : ControllerBase
    {
        private readonly IToppingService _toppingService;

        public ToppingsController(IToppingService toppingService)
        {
            _toppingService = toppingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToppingDto>>> GetToppings()
        {
            var toppings = await _toppingService.GetAllToppingsAsync();
            return Ok(toppings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToppingDto>> GetTopping(int id)
        {
            var topping = await _toppingService.GetToppingByIdAsync(id);

            if (topping == null)
            {
                return NotFound($"Topping with ID {id} not found.");
            }

            return Ok(topping);
        }

        [HttpPost]
        public async Task<ActionResult<ToppingDto>> CreateTopping(CreateToppingDto createToppingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _toppingService.ToppingExistsAsync(createToppingDto.Name))
            {
                return Conflict($"A topping with the name '{createToppingDto.Name}' already exists.");
            }

            try
            {
                var topping = await _toppingService.CreateToppingAsync(createToppingDto);
                return CreatedAtAction(nameof(GetTopping), new { id = topping.Id }, topping);
            }
            catch (DbUpdateException)
            {
                return Conflict("A topping with this name already exists.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ToppingDto>> UpdateTopping(int id, UpdateToppingDto updateToppingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _toppingService.ToppingExistsAsync(updateToppingDto.Name, id))
            {
                return Conflict($"A topping with the name '{updateToppingDto.Name}' already exists.");
            }

            try
            {
                var topping = await _toppingService.UpdateToppingAsync(id, updateToppingDto);

                if (topping == null)
                {
                    return NotFound($"Topping with ID {id} not found.");
                }

                return Ok(topping);
            }
            catch (DbUpdateException)
            {
                return Conflict("A topping with this name already exists.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTopping(int id)
        {
            var result = await _toppingService.DeleteToppingAsync(id);

            if (!result)
            {
                return NotFound($"Topping with ID {id} not found.");
            }

            return NoContent();
        }
    }
}