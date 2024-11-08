using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class PlantRepository : IPlantRepository
    {
        private readonly BonsaiDbcontext _context;

        public PlantRepository(BonsaiDbcontext context)
        {
            _context = context;
        }

        public async Task<int> AddPlant(Plant plant)
        {
            var newPlantId = new SqlParameter("@NewPlantId", SqlDbType.Int) { Direction = ParameterDirection.Output };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddPlant @CategoryId, @GardenId, @PlantName, @Price, @IsAvailable, @Description",
                new SqlParameter("@CategoryId", plant.CategoryId),
                new SqlParameter("@GardenId", plant.GardenId),
                new SqlParameter("@PlantName", plant.PlantName),
                new SqlParameter("@Price", plant.Price),
                new SqlParameter("@IsAvailable", plant.IsAvailable),
                new SqlParameter("@Description", plant.Description)
            );

            return (int)newPlantId.Value;
        }

        public async Task<IEnumerable<Plant>> GetPlants()
        {
            return await _context.Plants.FromSqlRaw("EXEC GetPlants").ToListAsync();
        }

        public async Task UpdatePlant(Plant plant)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdatePlant @PlantId, @CategoryId, @GardenId, @PlantName, @Price, @IsAvailable, @Description",
                new SqlParameter("@PlantId", plant.PlantId),
                new SqlParameter("@CategoryId", plant.CategoryId),
                new SqlParameter("@GardenId", plant.GardenId),
                new SqlParameter("@PlantName", plant.PlantName),
                new SqlParameter("@Price", plant.Price),
                new SqlParameter("@IsAvailable", plant.IsAvailable),
                new SqlParameter("@Description", plant.Description)
            );
        }

        public async Task DeletePlant(int plantId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeletePlant @PlantId",
                new SqlParameter("@PlantId", plantId)
            );
        }
    }
}
