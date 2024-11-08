using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IPlantRepository
    {
        Task<int> AddPlant(Plant plant);
        Task<IEnumerable<Plant>> GetPlants();
        Task UpdatePlant(Plant plant);
        Task DeletePlant(int plantId);
    }
}
