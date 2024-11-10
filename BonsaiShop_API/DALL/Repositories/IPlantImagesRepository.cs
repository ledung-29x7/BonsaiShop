using BonsaiShop_API.Areas.Garden.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IPlantImagesRepository
    {
        Task AddPlantImageAsync(PlantImage plantImage);
        Task DeletePlantImageAsync(int plantImageId);
        Task<List<PlantImage>> GetImagesByPlantIdAsync(int plantId);
        Task<PlantImage> GetPlantImageByIdAsync(int plantImageId);
    }
}
