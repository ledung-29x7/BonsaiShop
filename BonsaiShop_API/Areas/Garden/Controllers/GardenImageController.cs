using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/gardens/images")]
    [ApiController]
    public class GardenImageController : ControllerBase
    {
        private readonly IGardenImagesReponsitory gardenImagesReponsitory;

        public GardenImageController(IGardenImagesReponsitory gardenImagesReponsitory)
        {
            this.gardenImagesReponsitory = gardenImagesReponsitory;
        }

        // Add Image
        [HttpPost]
        public async Task<IActionResult> AddImage([FromBody] GardenImages gardenImages)
        {
            if (gardenImages == null)
                return BadRequest("Image data is required.");

            await gardenImagesReponsitory.AddGardenImageAsync(gardenImages);
            return Ok("Image added successfully.");
        }

        // Delete Image
        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteGardenImage(int imageId)
        {
            await gardenImagesReponsitory.DeleteGardenImageAsync(imageId);
            return Ok(new { Message = "Image deleted successfully" });
        }

        // Get all images for a specific garden by GardenId
        [HttpGet("garden/{gardenId}")]
        public async Task<ActionResult<List<GardenImages>>> GetImagesByGardenId(int gardenId)
        {
            var images = await gardenImagesReponsitory.GetImagesByGardenIdAsync(gardenId);
            return Ok(images);
        }

        // Get a specific image by GardenImageId
        [HttpGet("{id}")]
        public async Task<ActionResult> GetImageById(int id)
        {
            var image = await gardenImagesReponsitory.GetGardenImageByIdAsync(id);
            if (image == null)
                return NotFound("Image not found.");

            try
            {
                // Convert the Base64 string to byte array
                byte[] imageBytes = Convert.FromBase64String(image.ImageBase64);

                string contentType = "application/octet-stream"; // Default to binary stream
                if (image.ImageBase64.StartsWith("/9j/"))  // JPEG signature
                {
                    contentType = "image/jpeg";
                }
                else if (image.ImageBase64.StartsWith("iVBORw0KGgo")) // PNG signature
                {
                    contentType = "image/png";
                }

                return File(imageBytes, contentType);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid Base64 format.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
