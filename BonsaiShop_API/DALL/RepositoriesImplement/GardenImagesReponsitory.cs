using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class GardenImagesReponsitory : IGardenImagesReponsitory
    {
        private readonly BonsaiDbcontext dbcontext;

        public GardenImagesReponsitory(BonsaiDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        // Add Garden Image
        public async Task AddGardenImageAsync(GardenImages gardenImages)
        {
            await dbcontext.Database.ExecuteSqlRawAsync(
              "EXEC AddGardenImage @GardenId, @ImageBase64, @Caption, @IsPrimary ",
              new SqlParameter("@GardenId", gardenImages.GardenId),
              new SqlParameter("@ImageBase64", gardenImages.ImageBase64),
              new SqlParameter("@Caption", gardenImages.Caption),
              new SqlParameter("@IsPrimary", gardenImages.IsPrimary)
            );
        }

        // Delete Garden Image
        public async Task DeleteGardenImageAsync(int imageId)
        {
            await dbcontext.Database.ExecuteSqlRawAsync(
                "EXEC DeletePlantImage @GardenImageId",
                new SqlParameter("@GardenImageId", imageId));
        }

        // Get Images by GardenId
        public async Task<List<GardenImages>> GetImagesByGardenIdAsync(int gardenId)
        {
            var images = await dbcontext.GardenImages
                .FromSqlRaw("EXEC GetImagesByGardenId @GardenId", new SqlParameter("@GardenId", gardenId))
                .ToListAsync();
            return images;
        }

        // Get a specific image by GardenImageId using the stored procedure
        public async Task<GardenImages> GetGardenImageByIdAsync(int gardenImageId)
        {
            var images = await dbcontext.GardenImages
         .FromSqlRaw("EXEC GetGardenImageById @GardenImageId",
             new SqlParameter("@GardenImageId", gardenImageId))
         .ToListAsync();  // Materializes the results into a list
            return images.FirstOrDefault();  
        }
    }
}
