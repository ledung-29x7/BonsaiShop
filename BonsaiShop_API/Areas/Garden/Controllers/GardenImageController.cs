using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.Areas.Service;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/gardens/{gardenId}/images")]
    [ApiController]
    public class GardenImageController : ControllerBase
    {
        private readonly IGardenImageRepository _gardenImageRepository;

        public GardenImageController(IGardenImageRepository gardenImageRepository)
        {
            _gardenImageRepository = gardenImageRepository;
        }

        //// POST: /api/gardens/{gardenId}/images
        //[HttpPost]
        //[Authorize(Roles = "ADMIN, GARDEN")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> AddImage(int gardenId, [FromForm] IFormFile imageFile, [FromForm] string caption, [FromForm] bool isPrimary)
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
        //    var gardenImageDto = new GardenImageDTO
        //    {
        //        ImageBase64 = imageBase64,
        //        Caption = caption,
        //        IsPrimary = isPrimary
        //    };

        //    // Lưu vào cơ sở dữ liệu thông qua repository
        //    await _gardenImageRepository.AddImageAsync(gardenId, gardenImageDto);

        //    return Ok(new { message = "Image added successfully." });
        //}


        // DELETE: /api/gardens/{gardenId}/images/{imageId}
        [HttpDelete("{imageId}")]
        [Authorize(Roles = "ADMIN, GARDEN")]

        public async Task<IActionResult> DeleteImage(int gardenId, int imageId)
        {
            await _gardenImageRepository.DeleteImageAsync(gardenId, imageId);
            return Ok(new { message = "Image deleted successfully." });
        }
    }
}
