 using BonsaiShop_API.Areas.Garden.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IPlantsRepository
    {
        public Task<Plants> AddPlantAsync(Plants plant);
        Task<IEnumerable<Plants>> GetAllPlantsAsync();
        Task<Plants> GetPlantByIdAsync(int plantId);
        Task UpdatePlantAsync(Plants plant);
        Task DeletePlantAsync(int plantId);
        Task<IEnumerable<PlantWithImageDto>> SearchPlantsAsync(string planName, string categoryName, decimal? minPrice, decimal? maxPrice ,bool IsAvailable);
    }
}
