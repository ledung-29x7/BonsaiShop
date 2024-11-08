using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class PlantsRepository : IPlantsRepository
    {
        private readonly BonsaiDbcontext _dbContext;

        public PlantsRepository(BonsaiDbcontext dbContext)
        {
            _dbContext = dbContext;
        }


        // Add a new plant using the AddPlant stored procedure
        public async Task<Plants> AddPlantAsync(Plants plant)
        {
            var categoryIdParam = new SqlParameter("@CategoryId", plant.CategoryId);
            var gardenIdParam = new SqlParameter("@GardenId", plant.GardenId);
            var plantNameParam = new SqlParameter("@PlantName", plant.PlantName);
            var priceParam = new SqlParameter("@Price", plant.Price);
            var isAvailableParam = new SqlParameter("@IsAvailable", plant.IsAvailable);
            var descriptionParam = new SqlParameter("@Description", plant.Description ?? (object)DBNull.Value);

            var plantIdParam = new SqlParameter
            {
                ParameterName = "@PlantId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.AddPlant @CategoryId, @GardenId, @PlantName, @Price, @IsAvailable, @Description, @PlantId OUTPUT",
                categoryIdParam, gardenIdParam, plantNameParam, priceParam, isAvailableParam, descriptionParam, plantIdParam);

            // Fetch the PlantId from the output parameter
            int plantId = (int)plantIdParam.Value;

            // Return the added plant with the correct PlantId
            plant.PlantId = plantId;
            return plant;
        }

        // Get all plants using the GetAllPlants stored procedure
        public async Task<IEnumerable<Plants>> GetAllPlantsAsync()
        {
            var plants = await _dbContext.Plants.FromSqlRaw("EXEC dbo.GetAllPlants").ToListAsync();
            return plants;
        }

        public async Task<Plants> GetPlantByIdAsync(int plantId)
        {
            var result = await _dbContext.Plants.FromSqlRaw(
                "EXEC GetPlantById @PlantId",
                new SqlParameter("@PlantId", plantId)
            ).ToListAsync();

            return result.FirstOrDefault();
        }


        // Update a plant using the UpdatePlant stored procedure
        public async Task UpdatePlantAsync(Plants plant)
        {
            var plantIdParam = new SqlParameter("@PlantId", plant.PlantId);
            var categoryIdParam = new SqlParameter("@CategoryId", plant.CategoryId);
            var gardenIdParam = new SqlParameter("@GardenId", plant.GardenId);
            var plantNameParam = new SqlParameter("@PlantName", plant.PlantName);
            var priceParam = new SqlParameter("@Price", plant.Price);
            var isAvailableParam = new SqlParameter("@IsAvailable", plant.IsAvailable);
            var descriptionParam = new SqlParameter("@Description", plant.Description ?? (object)DBNull.Value);

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.UpdatePlant @PlantId, @CategoryId, @GardenId, @PlantName, @Price, @IsAvailable, @Description",
                plantIdParam, categoryIdParam, gardenIdParam, plantNameParam, priceParam, isAvailableParam, descriptionParam);
        }

        // Delete a plant using the DeletePlant stored procedure
        public async Task DeletePlantAsync(int plantId)
        {
            var plantIdParam = new SqlParameter("@PlantId", plantId);
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.DeletePlant @PlantId", plantIdParam);
        }
        public async Task<IEnumerable<PlantWithImageDto>> SearchPlantsAsync(string plantName, string categoryName, decimal? minPrice, decimal? maxPrice, bool IsAvailable)
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                var query = "EXEC SearchPlants @PlantName, @CategoryName, @MinPrice, @MaxPrice, @IsAvailable";

                var parameters = new DynamicParameters();
                parameters.Add("@PlantName", plantName, DbType.String);
                parameters.Add("@CategoryName", categoryName, DbType.String);
                parameters.Add("@MinPrice", minPrice, DbType.Decimal);  
                parameters.Add("@MaxPrice", maxPrice, DbType.Decimal);
                parameters.Add("@IsAvailable", IsAvailable, DbType.Boolean);

                await connection.OpenAsync();
                var plants = await connection.QueryAsync<PlantWithImageDto>(query, parameters);
                return plants;
            }
        }
    }
}
