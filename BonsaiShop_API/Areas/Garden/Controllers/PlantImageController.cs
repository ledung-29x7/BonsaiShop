using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.Areas.Service;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/plants/{plantId}/images")]
    [ApiController]
    public class PlantImageController : ControllerBase
    {
        private readonly IPlantImageRepository _plantImageRepository;

        public PlantImageController(IPlantImageRepository plantImageRepository)
        {
            _plantImageRepository = plantImageRepository;
        }

        //[HttpPost]
        //[Authorize(Roles = "ADMIN, GARDEN")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> AddImage(int plantId, [FromForm] IFormFile imageFile, [FromForm] string caption, [FromForm] bool isPrimary)
        //{
        //    if (imageFile == null || imageFile.Length == 0)
        //    {
        //        return BadRequest(new { message = "Image file is required." });
        //    }

        //    // Chuyển đổi file ảnh thành Base64
        //    string imageBase64;
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        await imageFile.CopyToAsync(memoryStream);
        //        imageBase64 = Convert.ToBase64String(memoryStream.ToArray());
        //    }

        //    // Tạo DTO với thông tin ảnh
        //    var plantImageDto = new PlantImageDTO
        //    {
        //        ImageBase64 = imageBase64,
        //        Caption = caption,
        //        IsPrimary = isPrimary
        //    };

        //    // Lưu vào cơ sở dữ liệu
        //    await _plantImageRepository.AddImageAsync(plantId, plantImageDto);

        //    return Ok(new { message = "Image added successfully." });
        //}

        // DELETE: /api/plants/{plantId}/images/{imageId}
        [HttpDelete("{imageId}")]
        [Authorize(Roles = "ADMIN, GARDEN")]
        public async Task<IActionResult> DeleteImage(int plantId, int imageId)
        {
            await _plantImageRepository.DeleteImageAsync(plantId, imageId);
            return Ok(new { message = "Image deleted successfully." });
        }
    }
}
