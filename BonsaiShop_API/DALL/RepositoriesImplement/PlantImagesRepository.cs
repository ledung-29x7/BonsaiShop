using BonsaiShop_API.Areas.Garden.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonsaiShop_API.DALL.Repositories
{
    public class PlantImagesRepository : IPlantImagesRepository
    {
        private readonly BonsaiDbcontext dbcontext;

        public PlantImagesRepository(BonsaiDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        // Add a Plant Image
        public async Task AddPlantImageAsync(PlantImage plantImage)
        {
            var parameters = new[]
            {
                new SqlParameter("@PlantId", plantImage.PlantId),
                new SqlParameter("@ImageBase64", plantImage.ImageBase64),
                new SqlParameter("@Caption", plantImage.Caption ?? (object)DBNull.Value),
                new SqlParameter("@IsPrimary", plantImage.IsPrimary)
            };

            await dbcontext.Database.ExecuteSqlRawAsync("EXEC AddPlantImage @PlantId, @ImageBase64, @Caption, @IsPrimary", parameters);
        }

        // Delete a Plant Image
        public async Task DeletePlantImageAsync(int plantImageId)
        {
            var parameters = new[]
            {
                new SqlParameter("@PlantImageId", plantImageId)
            };

            await dbcontext.Database.ExecuteSqlRawAsync("EXEC DeletePlantImage @PlantImageId", parameters);
        }

        // Get all Plant Images by PlantId
        public async Task<List<PlantImage>> GetImagesByPlantIdAsync(int plantId)
        {
            var parameters = new[]
            {
                new SqlParameter("@PlantId", plantId)
            };

            return await dbcontext.Set<PlantImage>()
                .FromSqlRaw("EXEC GetImagesByPlantId @PlantId", parameters)
                .ToListAsync();
        }

        public async Task<PlantImage> GetPlantImageByIdAsync(int plantImageId)
        {

            var images = await dbcontext.Set<PlantImage>()
              .FromSqlRaw("EXEC GetPlantImageById @PlantImageId",
                  new SqlParameter("@PlantImageId", plantImageId))
              .ToListAsync();  // Materializes the results into a list
            return images.FirstOrDefault(); 
        }
    }
}
