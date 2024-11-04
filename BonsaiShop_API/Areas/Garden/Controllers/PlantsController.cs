using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
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

        // GET /api/plants/search - Search plants with filters
        [HttpGet("search")]
        public async Task<IActionResult> SearchPlants(
            [FromQuery] string plantName,
            [FromQuery] string? categoryName,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool isAvailable = true)
        {
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
