using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class GardenImageRepository : IGardenImageRepository
    {
        private readonly BonsaiDbcontext _dbcontext;

        public GardenImageRepository(BonsaiDbcontext dbcontext) 
        {
            _dbcontext = dbcontext;
        }
        public async  Task AddImageAsync(int gardenId, GardenImageDTO gardenImageDto)
        {
            var gardenIdParam = new SqlParameter("@GardenId", gardenId);
            var imageBase64Param = new SqlParameter("@ImageBase64", gardenImageDto.ImageBase64);
            var captionParam = new SqlParameter("@Caption", gardenImageDto.Caption);
            var isPrimaryParam = new SqlParameter("@IsPrimary", gardenImageDto.IsPrimary);

            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC AddGardenImage @GardenId, @ImageBase64, @Caption, @IsPrimary",
                gardenIdParam, imageBase64Param, captionParam, isPrimaryParam);
        }

        public async Task DeleteImageAsync(int gardenId, int imageId)
        {
            var gardenIdParam = new SqlParameter("@GardenId", gardenId);
            var imageIdParam = new SqlParameter("@ImageId", imageId);

            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC DeleteGardenImage @GardenId, @ImageId", gardenIdParam, imageIdParam);
        }
    }
}
