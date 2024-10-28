using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class PlantRepository : IPlantRepository
    {
        public Task AddPlants(List<Plant> plants)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlant(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Plant>> GetAllPlants()
        {
            throw new NotImplementedException();
        }

        public Task<Plant> GetPlantById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlants(List<Plant> plants)
        {
            throw new NotImplementedException();
        }
    }
}
