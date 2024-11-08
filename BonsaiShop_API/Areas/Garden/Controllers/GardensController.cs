using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class GardensController : ControllerBase
{
    private readonly IGardensReponsitory _gardenRepository;

    public GardensController(IGardensReponsitory gardenRepository)
    {
        _gardenRepository = gardenRepository;
    }

    // POST /api/gardens - Register a garden
    [HttpPost]
    public async Task<ActionResult<Garden>> CreateGarden(Garden garden)
    {
        var createdGarden = await _gardenRepository.CreateGardenAsync(garden);

        if (createdGarden == null)
        {
            return BadRequest("Failed to create garden.");
        }

        // Return a 201 Created response with the created garden object
        return CreatedAtAction(nameof(CreateGarden), new { id = createdGarden.GardenId }, createdGarden);
    }


    // GET /api/gardens - Retrieve all gardens
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Garden>>> GetAllGardens()
    {
        var gardens = await _gardenRepository.GetAllGardensAsync();
        return Ok(gardens);
    }

    // GET /api/gardens/{id} - Retrieve a specific garden by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Garden>> GetGardenById(int id)
    {
        var garden = await _gardenRepository.GetGardenByIdAsync(id);
        if (garden == null)
        {
            return NotFound();
        }
        return Ok(garden);
    }

    // PUT /api/gardens/{id} - Update garden information
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGarden(int id, Garden garden)
    {
        // Check if the GardenId in the body matches the id in the URL
        if (id != garden.GardenId)
        {
            return BadRequest("Garden ID mismatch.");
        }

        // Check if the garden exists
        var existingGarden = await _gardenRepository.GetGardenByIdAsync(id);
        if (existingGarden == null)
        {
            return NotFound();
        }

        // Validate model state
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Proceed with updating the garden
        await _gardenRepository.UpdateGardenAsync(garden);
        return NoContent();
    }

    // DELETE /api/gardens/{id} - Delete a garden
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGarden(int id)
    {
        var existingGarden = await _gardenRepository.GetGardenByIdAsync(id);
        if (existingGarden == null)
        {
            return NotFound();
        }

        await _gardenRepository.DeleteGardenAsync(id);
        return NoContent();
    }
}
