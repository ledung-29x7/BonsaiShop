using AutoMapper;
using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class PlantRepository : IPlantRepository
    {
        private readonly BonsaiDbcontext _dbcontext;
        private readonly IMapper _mapper;

        public PlantRepository(BonsaiDbcontext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        // Thêm cây cảnh
        public async Task AddPlantAsync(PlantDTO plantDto)
        {
            var categoryIdParam = new SqlParameter("@CategoryId", plantDto.CategoryId);
            var gardenIdParam = new SqlParameter("@GardenId", plantDto.GardenId);
            var plantNameParam = new SqlParameter("@PlantName", plantDto.PlantName);
            var priceParam = new SqlParameter("@Price", plantDto.Price);
            var isAvailableParam = new SqlParameter("@IsAvailable", plantDto.IsAvailable);
            var descriptionParam = new SqlParameter("@Description", plantDto.Description);

            await _dbcontext.Database.ExecuteSqlRawAsync(
                "EXEC AddPlant @CategoryId, @GardenId, @PlantName, @Price, @IsAvailable, @Description",
                categoryIdParam, gardenIdParam, plantNameParam, priceParam, isAvailableParam, descriptionParam);
        }

        // Lấy danh sách cây cảnh
        //public async Task<List<PlantDTO>> GetPlantsAsync()
        //{
        //    var plants = await _dbcontext.Plants
        //        .FromSqlRaw("EXEC GetPlants")
        //        .ToListAsync();

        //    return _mapper.Map<List<PlantDTO>>(plants);
        //}

        // Cập nhật cây cảnh
        public async Task UpdatePlantAsync(int id, PlantDTO plantDto)
        {
            var plantIdParam = new SqlParameter("@PlantId", id);
            var categoryIdParam = new SqlParameter("@CategoryId", plantDto.CategoryId);
            var gardenIdParam = new SqlParameter("@GardenId", plantDto.GardenId);
            var plantNameParam = new SqlParameter("@PlantName", plantDto.PlantName);
            var priceParam = new SqlParameter("@Price", plantDto.Price);
            var isAvailableParam = new SqlParameter("@IsAvailable", plantDto.IsAvailable);
            var descriptionParam = new SqlParameter("@Description", plantDto.Description);

            await _dbcontext.Database.ExecuteSqlRawAsync(
                "EXEC UpdatePlant @PlantId, @CategoryId, @GardenId, @PlantName, @Price, @IsAvailable, @Description",
                plantIdParam, categoryIdParam, gardenIdParam, plantNameParam, priceParam, isAvailableParam, descriptionParam);
        }

        // Xóa cây cảnh
        public async Task DeletePlantAsync(int id)
        {
            var plantIdParam = new SqlParameter("@PlantId", id);

            await _dbcontext.Database.ExecuteSqlRawAsync(
                "EXEC DeletePlant @PlantId", plantIdParam);
        }

        public async Task<PlantDetailDTO> GetPlantDetailsAsync(int plantId)
        {
            var plantIdParam = new SqlParameter("@PlantId", plantId);

            var plant = await _dbcontext.Plants
                .FromSqlRaw("EXEC GetPlantDetails @PlantId", plantIdParam)
                .Include(p => p.Images) // Assumes Plant entity has a navigation property `Images`
                .FirstOrDefaultAsync();

            if (plant == null) return null;

            return _mapper.Map<PlantDetailDTO>(plant);
        }

        public async Task<List<Plants>> GetPlantsAsync()
        {
            var plants = await _dbcontext.Plants
                .FromSqlRaw("EXEC GetPlants")
                .ToListAsync();
            return plants;
        }
    }
}
