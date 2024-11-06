using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BonsaiShop_API;
using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;

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

        // GET: api/Plant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants()
        {
            var plant = await _plantRepository.GetAllPlants();
            return Ok(plant);
        }

        // GET: api/Plant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(int id)
        {
            var plant = await _plantRepository.GetPlantById(id);

            if (plant == null)
            {
                return NotFound();
            }

            return plant;
        }

        // PUT: api/Plant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlant(int id, Plant plant)
        {
            var plants = await _plantRepository.GetAllPlants();
            if (id != plant.PlantId)
            {
                return BadRequest();
            }

            try
            {
                await _plantRepository.UpdatePlants(plant);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!plants.Any(e => e.PlantId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Plant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plant>> PostPlant(List<Plant> plant)
        {
            await _plantRepository.AddPlants(plant);
            return Ok();
        }

        // DELETE: api/Plant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            
            if (id < 0)
            {
                return NotFound();
            }
             await _plantRepository.DeletePlant(id);
            
            return NoContent();
        }

    }
}
