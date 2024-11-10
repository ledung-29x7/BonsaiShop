using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using BonsaiShop_API.DALL.RepositoriesImplement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/gardens/{gardenId}/images")]
    [ApiController]
    public class GardenImageController : ControllerBase
    {
        private readonly IGardenImagesReponsitory gardenImagesReponsitory;

        public GardenImageController(IGardenImagesReponsitory gardenImageRepository)
        {
            this.gardenImagesReponsitory = gardenImageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddGardenImage(
    [FromBody] int GardenId,         // GardenId from the form
    [FromBody] string Caption,      // Caption from the form
    [FromBody] bool IsPrimary,      // IsPrimary from the form
    [FromBody] IFormFile ImageFile) // ImageFile from the form (instead of FromBody)
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                return BadRequest(new { Message = "ImageFile is required and cannot be empty." });
            }

            try
            {
                // Convert the image to Base64 string
                string base64String;
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    base64String = Convert.ToBase64String(memoryStream.ToArray());
                }

                var gardenImages = new GardenImages
                {
                    GardenId = GardenId,
                    Caption = Caption,
                    IsPrimary = IsPrimary,
                    ImageBase64 = base64String,
                    UploadedAt = DateTime.UtcNow // Set the upload time
                };

                // Save the garden image to the database
                await gardenImagesReponsitory.AddGardenImageAsync(gardenImages);

                // Return the success response with the garden image details
                var jsonResponse = new
                {
                    gardenImageId = gardenImages.GardenImageId,
                    gardenId = gardenImages.GardenId,
                    imageBase64 = gardenImages.ImageBase64,  // Return the Base64 string
                    caption = gardenImages.Caption,
                    isPrimary = gardenImages.IsPrimary,
                    uploadedAt = gardenImages.UploadedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                };

                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Details = ex.Message });
            }
        }



        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteGardenImage(int imageId)
        {
            await gardenImagesReponsitory.DeleteGardenImageAsync(imageId);
            return Ok(new { Message = "Image deleted successfully" });
        }
    }
}
