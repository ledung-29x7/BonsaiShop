using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IPlantRepository
    {
        Task<List<Plant>> GetAllPlants();
        Task<Plant> GetPlantById(int id);
        Task UpdatePlants (Plant plant);
        Task AddPlants (List<Plant> plants);
        Task DeletePlant(int id);

    }
}
