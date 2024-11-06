 using BonsaiShop_API.Areas.Garden.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IPlantsRepository
    {
        Task<IEnumerable<PlantWithImageDto>> SearchPlantsAsync(string planName, string categoryName, decimal? minPrice, decimal? maxPrice ,bool isAvailable);
    }
}
