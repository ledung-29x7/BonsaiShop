using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class PlantRepository : IPlantRepository
    {
        private BonsaiDbcontext dbcontext;
        public PlantRepository(BonsaiDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task AddPlants(List<Plant> plants)
        {
            var transaction = await dbcontext.Database.BeginTransactionAsync();
            try
            {
                foreach (var plant in plants)
                {
                    var categoryId = new SqlParameter("@CategoryId", plant.CategoryId);
                    var gardenId = new SqlParameter("@GardenId", plant.GardenId);
                    var plantName = new SqlParameter("@PlantName", plant.PlantName);
                    var price = new SqlParameter("@Price", plant.Price);
                    var isAvailable = new SqlParameter("@IsAvailable", plant.IsAvailable);
                    var description = new SqlParameter("@Description", plant.Description);
                    var createAt = new SqlParameter("@CreateAt", plant.CreatedAt);
                    await dbcontext.Database.ExecuteSqlRawAsync("EXEC dbo.AddPlants @CategoryId, @GardenId, @PlantName, @Price, @IsAvailable, @Description, @CreateAt",
                        categoryId,gardenId,plantName, price,isAvailable,description,createAt
                        );
                }
                await transaction.CommitAsync();
            }
            catch 
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeletePlant(int id)
        {
            var idPlant = new SqlParameter("@PlantId", id);
            await dbcontext.Database.ExecuteSqlRawAsync("EXEC dbo.DeletePlants @PlantId ", idPlant);
        }

        public async Task<List<Plant>> GetAllPlants()
        {
            return await dbcontext.Plants.FromSqlRaw("EXEC dbo.GetAllPlant").ToListAsync();
        }

        public async Task<Plant> GetPlantById(int id)
        {
            var idPlant = new SqlParameter("@PlantId", id);
            var plantst = await dbcontext.Plants.FromSqlRaw("EXEC dbo.GetById", idPlant).ToListAsync();
            return plantst.FirstOrDefault();
        }

        public async Task UpdatePlants(Plant plant)
        {
            var transaction = await dbcontext.Database.BeginTransactionAsync();
            try
            {
                
                    var categoryId = new SqlParameter("@CategoryId", plant.CategoryId);
                    var gardenId = new SqlParameter("@GardenId", plant.GardenId);
                    var plantName = new SqlParameter("@PlantName", plant.PlantName);
                    var price = new SqlParameter("@Price", plant.Price);
                    var isAvailable = new SqlParameter("@IsAvailable", plant.IsAvailable);
                    var description = new SqlParameter("@Description", plant.Description);
                    var createAt = new SqlParameter("@CreatAt", plant.CreatedAt);
                    await dbcontext.Database.ExecuteSqlRawAsync("EXEC @CategoryId, @GardenId, @PlantName, @Price, @IsAvailable, @Description, @CreateAt",
                        categoryId, gardenId, plantName, price, isAvailable, description, createAt
                        );
                
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
