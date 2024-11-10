using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/plants/images")]
    [ApiController]
    public class PlantImageController : ControllerBase
    {
        private readonly IPlantImagesRepository _plantImagesRepository;

        public PlantImageController(IPlantImagesRepository plantImagesRepository)
        {
            _plantImagesRepository = plantImagesRepository;
        }

        // Add a Plant Image
        [HttpPost]
        public async Task<IActionResult> AddImage([FromBody] PlantImage plantImage)
        {
            if (plantImage == null)
                return BadRequest("Image data is required.");

            try
            {
                await _plantImagesRepository.AddPlantImageAsync(plantImage);
                return Ok("Image added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete a Plant Image
        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeletePlantImage(int imageId)
        {
            try
            {
                await _plantImagesRepository.DeletePlantImageAsync(imageId);
                return Ok(new { Message = "Image deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get all images for a specific plant by PlantId
        [HttpGet("plant/{plantId}")]
        public async Task<ActionResult<List<PlantImage>>> GetImagesByPlantId(int plantId)
        {
            try
            {
                var images = await _plantImagesRepository.GetImagesByPlantIdAsync(plantId);
                return Ok(images);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get a specific image by PlantImageId
        [HttpGet("{id}")]
        public async Task<ActionResult> GetImageById(int id)
        {
            try
            {
                var image = await _plantImagesRepository.GetPlantImageByIdAsync(id);
                if (image == null)
                    return NotFound("Image not found.");

                byte[] imageBytes = Convert.FromBase64String(image.ImageBase64);
                string contentType = "application/octet-stream"; // Default to binary stream

                if (image.ImageBase64.StartsWith("/9j/"))  // JPEG signature
                    contentType = "image/jpeg";
                else if (image.ImageBase64.StartsWith("iVBORw0KGgo")) // PNG signature
                    contentType = "image/png";

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
