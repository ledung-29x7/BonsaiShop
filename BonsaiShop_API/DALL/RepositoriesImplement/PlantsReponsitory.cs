using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Dapper;
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

        public async Task<IEnumerable<PlantWithImageDto>> SearchPlantsAsync(string plantName, string categoryName, decimal? minPrice, decimal? maxPrice, bool isAvailable)
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                var query = "EXEC SearchPlants @PlantName, @CategoryName, @MinPrice, @MaxPrice, @IsAvailable";

                var parameters = new DynamicParameters();
                parameters.Add("@PlantName", plantName, DbType.String);
                parameters.Add("@CategoryName", categoryName, DbType.String);
                parameters.Add("@MinPrice", minPrice, DbType.Decimal);  
                parameters.Add("@MaxPrice", maxPrice, DbType.Decimal);
                parameters.Add("@IsAvailable", isAvailable, DbType.Boolean);

                await connection.OpenAsync();
                var plants = await connection.QueryAsync<PlantWithImageDto>(query, parameters);
                return plants;
            }
        }
    }
}
