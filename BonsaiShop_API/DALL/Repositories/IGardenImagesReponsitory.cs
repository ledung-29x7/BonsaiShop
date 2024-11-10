using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IGardenImagesReponsitory
    {
        Task AddGardenImageAsync(GardenImages gardenImages);
        Task DeleteGardenImageAsync(int imageId);
        Task<List<GardenImages>> GetImagesByGardenIdAsync(int gardenId); // Corrected return type
        Task<GardenImages> GetGardenImageByIdAsync(int gardenImageId);  // Corrected parameter name
    }
}
