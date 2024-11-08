using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IGardenRepository
    {
        Task<Gardens> CreateGardenAsync(GardenCreateDto gardenCreateDto);
        Task<List<Gardens>> GetGardensAsync();
        Task<Gardens> UpdateGardenAsync(int gardenId, GardenCreateDto gardenCreateDto);
        Task<bool> DeleteGardenAsync(int gardenId);
    }
}
