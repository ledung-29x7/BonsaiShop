using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IPlantRepository
    {
        Task AddPlantAsync(PlantDTO plantDto);
        Task<List<Plants>> GetPlantsAsync();
        Task UpdatePlantAsync(int id, PlantDTO plantDto);
        Task DeletePlantAsync(int id);
        Task<PlantDetailDTO> GetPlantDetailsAsync(int plantId);
    }
}
