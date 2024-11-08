using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IPlantImageRepository
    {
        Task AddImageAsync(int plantId, PlantImageDTO plantImageDto);
        Task DeleteImageAsync(int plantId, int imageId);
    }
}
