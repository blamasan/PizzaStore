using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models.DTOs;
using PizzaStore.Services;

namespace PizzaStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;

        public PizzasController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaDto>>> GetPizzas()
        {
            var pizzas = await _pizzaService.GetAllPizzasAsync();
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaDto>> GetPizza(int id)
        {
            var pizza = await _pizzaService.GetPizzaByIdAsync(id);

            if (pizza == null)
            {
                return NotFound($"Pizza with ID {id} not found.");
            }

            return Ok(pizza);
        }

        [HttpPost]
        public async Task<ActionResult<PizzaDto>> CreatePizza(CreatePizzaDto createPizzaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _pizzaService.PizzaExistsAsync(createPizzaDto.Name))
            {
                return Conflict($"A pizza with the name '{createPizzaDto.Name}' already exists.");
            }

            try
            {
                var pizza = await _pizzaService.CreatePizzaAsync(createPizzaDto);
                return CreatedAtAction(nameof(GetPizza), new { id = pizza.Id }, pizza);
            }
            catch (DbUpdateException)
            {
                return Conflict("A pizza with this name already exists.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PizzaDto>> UpdatePizza(int id, UpdatePizzaDto updatePizzaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _pizzaService.PizzaExistsAsync(updatePizzaDto.Name, id))
            {
                return Conflict($"A pizza with the name '{updatePizzaDto.Name}' already exists.");
            }

            try
            {
                var pizza = await _pizzaService.UpdatePizzaAsync(id, updatePizzaDto);

                if (pizza == null)
                {
                    return NotFound($"Pizza with ID {id} not found.");
                }

                return Ok(pizza);
            }
            catch (DbUpdateException)
            {
                return Conflict("A pizza with this name already exists.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePizza(int id)
        {
            var result = await _pizzaService.DeletePizzaAsync(id);

            if (!result)
            {
                return NotFound($"Pizza with ID {id} not found.");
            }

            return NoContent();
        }
    }
}