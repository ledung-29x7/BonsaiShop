using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using BonsaiShop_API.DALL.RepositoriesImplement;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/plants")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly IPlantsRepository plantsRepository;

        public PlantsController(IPlantsRepository plantsRepository)
        {
            this.plantsRepository = plantsRepository;
        }

        // POST /api/plants - Add a new plant
        [HttpPost]
        public async Task<ActionResult<Plants>> CreatePlant(Plants plant)
        {
            var createdPlant = await plantsRepository.AddPlantAsync(plant);
            return CreatedAtAction(nameof(GetPlantById), new { id = createdPlant.PlantId }, createdPlant);
        }

        // GET /api/plants - Get all plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plants>>> GetAllPlants()
        {
            var plants = await plantsRepository.GetAllPlantsAsync();
            return Ok(plants);
        }

        // GET /api/plants/{id} - Get a specific plant by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Plants>> GetPlantById(int id)
        {
            var plant = await plantsRepository.GetPlantByIdAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            return Ok(plant);
        }

        // PUT /api/plants/{id} - Update a plant
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlant(int id, Plants plant)
        {
            if (id != plant.PlantId)
            {
                return BadRequest("Plant ID mismatch.");
            }

            var existingPlant = await plantsRepository.GetPlantByIdAsync(id);
            if (existingPlant == null)
            {
                return NotFound();
            }

            await plantsRepository.UpdatePlantAsync(plant);
            return NoContent();
        }

        // DELETE /api/plants/{id} - Delete a plant
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var existingPlant = await plantsRepository.GetPlantByIdAsync(id);
            if (existingPlant == null)
            {
                return NotFound();
            }

            await plantsRepository.DeletePlantAsync(id);
            return NoContent();
        }
        // GET /api/plants/search - Search plants with filters
        [HttpGet("search")]
        public async Task<IActionResult> SearchPlants(
            [FromQuery] string plantName = null,
            [FromQuery] string categoryName = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] bool isAvailable = true)
        {
            // Kiểm tra ít nhất một tiêu chí được cung cấp
            if (string.IsNullOrEmpty(plantName) && string.IsNullOrEmpty(categoryName) && !minPrice.HasValue && !maxPrice.HasValue)
            {
                return BadRequest(new { status = "error", message = "Please provide at least one search criterion." });
            }

            var plants = await plantsRepository.SearchPlantsAsync(
                plantName, categoryName, minPrice, maxPrice, isAvailable);

            if (plants == null || !plants.Any())
                return NotFound(new { status = "error", message = "No plants found." });

            var plantDtos = plants.Select(p => new PlantDto
            {
                PlantId = p.PlantId,
                Name = p.PlantName,
                Price = p.Price,
                CategoryName = p.CategoryName,
                ImageBase64 = p.ImageBase64
            }).ToList();

            return Ok(plantDtos);
        }
    }
}
