using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IPlantRepository _plantRepository;

        public PlantController(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        // POST: /api/plants
        [HttpPost]
        [Authorize(Roles = "ADMIN, GARDEN")]
        public async Task<IActionResult> AddPlant([FromBody] PlantDTO plantDto)
        {
            await _plantRepository.AddPlantAsync(plantDto);
            return Ok(new { message = "Plant added successfully." });
        }

        // GET: /api/plants
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlants()
        {
            var plants = await _plantRepository.GetPlantsAsync();
            return Ok(plants);
        }

        // PUT: /api/plants/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN, GARDEN")]
        public async Task<IActionResult> UpdatePlant(int id, [FromBody] PlantDTO plantDto)
        {
            await _plantRepository.UpdatePlantAsync(id, plantDto);
            return Ok(new { message = "Plant updated successfully." });
        }

        // DELETE: /api/plants/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN, GARDEN")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            await _plantRepository.DeletePlantAsync(id);
            return Ok(new { message = "Plant deleted successfully." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlantDetails(int id)
        {
            var plantDetail = await _plantRepository.GetPlantDetailsAsync(id);

            if (plantDetail == null)
            {
                return NotFound(new { message = "Plant not found." });
            }

            return Ok(plantDetail);
        }

    }
}
