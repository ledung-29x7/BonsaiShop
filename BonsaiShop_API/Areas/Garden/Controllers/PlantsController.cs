using BonsaiShop_API;
using BonsaiShop_API.Areas.Garden.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PlantsController : ControllerBase
{
    private readonly BonsaiDbcontext _context;

    public PlantsController(BonsaiDbcontext context)
    {
        _context = context;
    }

    // POST /api/plants - Thêm cây cảnh mới
    [HttpPost]
    public async Task<IActionResult> AddPlant([FromBody] Plant plant)
    {
        if (plant == null)
            return BadRequest("Invalid plant data.");

        _context.Plants.Add(plant);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPlantById), new { id = plant.PlantId }, plant);
    }

    // GET /api/plants - Lấy danh sách cây cảnh
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Plant>>> GetPlants()
    {
        var plants = await _context.Plants.ToListAsync();
        return Ok(plants);
    }

    // GET /api/plants/{id} - Lấy thông tin cây cảnh theo ID
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Plant>> GetPlantById(int id)
    {
        var plant = await _context.Plants.FindAsync(id);
        if (plant == null)
            return NotFound();

        return Ok(plant);
    }

    // PUT /api/plants/{id} - Cập nhật thông tin cây cảnh
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePlant(int id, [FromBody] Plant plant)
    {
        if (id != plant.PlantId)
            return BadRequest("Plant ID mismatch.");

        var existingPlant = await _context.Plants.FindAsync(id);
        if (existingPlant == null)
            return NotFound();

        existingPlant.PlantName = plant.PlantName;
        existingPlant.CategoryId = plant.CategoryId;
        existingPlant.GardenId = plant.GardenId;
        existingPlant.Price = plant.Price;
        existingPlant.IsAvailable = plant.IsAvailable;
        existingPlant.Description = plant.Description;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/plants/{id} - Xóa cây cảnh
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePlant(int id)
    {
        var plant = await _context.Plants.FindAsync(id);
        if (plant == null)
            return NotFound();

        _context.Plants.Remove(plant);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
