using BonsaiShop_API.Areas.Garden.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GardensController : ControllerBase
    {
        private readonly BonsaiDbcontext _context;

        public GardensController(BonsaiDbcontext context)
        {
            _context = context;
        }

        // POST /api/Gardens - Thêm nhà vườn mới
        [HttpPost]
        public async Task<IActionResult> AddGarden([FromBody] Models.Garden garden)
        {
            if (garden == null)
                return BadRequest("Invalid garden data.");

            _context.Gardens.Add(garden);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGardenById), new { id = garden.GardenId }, garden);
        }

        // GET /api/Gardens - Lấy danh sách nhà vườn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Garden>>> GetGardens()
        {
            var gardens = await _context.Gardens.ToListAsync();
            return Ok(gardens);
        }

        // GET /api/Gardens/{id} - Lấy thông tin nhà vườn theo ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Models.Garden>> GetGardenById(int id)
        {
            var garden = await _context.Gardens.FindAsync(id);
            if (garden == null)
                return NotFound();

            return Ok(garden);
        }

        // PUT /api/Gardens/{id} - Cập nhật thông tin nhà vườn
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGarden(int id, [FromBody] Models.Garden garden)
        {
            if (id != garden.GardenId)
                return BadRequest("Garden ID mismatch.");

            var existingGarden = await _context.Gardens.FindAsync(id);
            if (existingGarden == null)
                return NotFound();

            existingGarden.GardenName = garden.GardenName;
            existingGarden.Address = garden.Address;
            existingGarden.Phone = garden.Phone;
            existingGarden.Description = garden.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/Gardens/{id} - Xóa nhà vườn
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGarden(int id)
        {
            var garden = await _context.Gardens.FindAsync(id);
            if (garden == null)
                return NotFound();

            _context.Gardens.Remove(garden);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
