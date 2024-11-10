using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class GardenImagesReponsitory : IGardenImagesReponsitory
    {
        
        private readonly BonsaiDbcontext dbcontext;

        public GardenImagesReponsitory(BonsaiDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task AddGardenImageAsync(GardenImages gardenImages)
        {var result = await dbcontext.GardenImages.FromSqlRaw(
              "EXEC AddGardenImage @GardenId, @ImageBase64, @Caption, @IsPrimary ",
              new SqlParameter("@GardenId" , gardenImages.GardenId) ,
              new SqlParameter("@ImageBase64", gardenImages.ImageBase64) ,
              new SqlParameter("@Caption", gardenImages.Caption) ,
              new SqlParameter("@IsPrimary", gardenImages.IsPrimary)  
            ).ToListAsync();
        }
      
        public async Task DeleteGardenImageAsync(int imageId)
        {
            await dbcontext.Database.ExecuteSqlRawAsync(
                "EXEC DeleteGardenImage @GardenImageId",
                new SqlParameter("@GardenImageId", imageId)) ;
          
        }

    }

    }
