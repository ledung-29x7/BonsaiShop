using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IGardenRepository
    {
        Task<int> AddGarden(Garden garden);
        Task<List<Garden>> GetGardens();
        Task UpdateGarden(Garden garden);
        Task DeleteGarden(int gardenId);
    }
}
