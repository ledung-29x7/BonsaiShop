using AutoMapper;
using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonsaiShop_API.Areas.Garden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GardensController : ControllerBase
    {
        private readonly IGardenRepository _gardenRepository;
        private readonly IMapper _mapper;

        public GardensController(IGardenRepository gardenRepository, IMapper mapper)
        {
            _gardenRepository = gardenRepository;
            _mapper = mapper;
        }

        // Đăng ký tài khoản nhà vườn
        [HttpPost]
        [Authorize(Roles = "ADMIN, GARDEN")]
        public async Task<IActionResult> CreateGarden([FromBody] GardenCreateDto gardenCreateDto)
        {
            var garden = await _gardenRepository.CreateGardenAsync(gardenCreateDto);
            var result = _mapper.Map<GardenDto>(garden);
            return Ok(result);
            
        }

        // Lấy danh sách nhà vườn
        [HttpGet]
        public async Task<IActionResult> GetGardens()
        {
            var gardens = await _gardenRepository.GetGardensAsync();
            var result = _mapper.Map<IEnumerable<GardenDto>>(gardens);
            return Ok(result);
        }

        // Cập nhật thông tin nhà vườn
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN, GARDEN")]
        public async Task<IActionResult> UpdateGarden(int id, [FromBody] GardenCreateDto gardenCreateDto)
        {
            var updatedGarden = await _gardenRepository.UpdateGardenAsync(id, gardenCreateDto);
            if (updatedGarden == null)
                return NotFound();
            var result = _mapper.Map<GardenDto>(updatedGarden);
            return Ok(result);
        }

        // Xóa nhà vườn
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteGarden(int id)
        {
            var result = await _gardenRepository.DeleteGardenAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}
