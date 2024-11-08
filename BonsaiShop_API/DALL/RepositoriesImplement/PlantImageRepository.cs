using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class PlantImageRepository : IPlantImageRepository
    {

        private readonly BonsaiDbcontext _dbcontext;

        public PlantImageRepository(BonsaiDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task AddImageAsync(int plantId, PlantImageDTO plantImageDto)
        {
            var plantIdParam = new SqlParameter("@PlantId", plantId);
            var imageBase64Param = new SqlParameter("@ImageBase64", plantImageDto.ImageBase64);
            var captionParam = new SqlParameter("@Caption", plantImageDto.Caption);
            var isPrimaryParam = new SqlParameter("@IsPrimary", plantImageDto.IsPrimary);

            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC AddPlantImage @PlantId, @ImageBase64, @Caption, @IsPrimary",
                plantIdParam, imageBase64Param, captionParam, isPrimaryParam);
        }

        public async Task DeleteImageAsync(int plantId, int imageId)
        {
            var plantIdParam = new SqlParameter("@PlantId", plantId);
            var imageIdParam = new SqlParameter("@ImageId", imageId);

            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC DeletePlantImage @PlantId, @ImageId", plantIdParam, imageIdParam);
        }
    }
}
