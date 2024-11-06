using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BonsaiShop_API;
using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GardenImageController : ControllerBase
    {
        private readonly BonsaiDbcontext _context;

        public GardenImageController(BonsaiDbcontext context)
        {
            _context = context;
        }

        // GET: api/GardenImage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GardenImage>>> GetGardenImages()
        {
            return await _context.GardenImages.ToListAsync();
        }

        // GET: api/GardenImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GardenImage>> GetGardenImage(int id)
        {
            var gardenImage = await _context.GardenImages.FindAsync(id);

            if (gardenImage == null)
            {
                return NotFound();
            }

            return gardenImage;
        }

        // PUT: api/GardenImage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGardenImage(int id, GardenImage gardenImage)
        {
            if (id != gardenImage.GardenImageId)
            {
                return BadRequest();
            }

            _context.Entry(gardenImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GardenImageExists(id))
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

        // POST: api/GardenImage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GardenImage>> PostGardenImage(GardenImage gardenImage)
        {
            _context.GardenImages.Add(gardenImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGardenImage", new { id = gardenImage.GardenImageId }, gardenImage);
        }

        // DELETE: api/GardenImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGardenImage(int id)
        {
            var gardenImage = await _context.GardenImages.FindAsync(id);
            if (gardenImage == null)
            {
                return NotFound();
            }

            _context.GardenImages.Remove(gardenImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GardenImageExists(int id)
        {
            return _context.GardenImages.Any(e => e.GardenImageId == id);
        }
    }
}
