using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IGardensReponsitory { 

        public  Task<IEnumerable<Garden>> GetAllGardensAsync();
        public Task<Garden> CreateGardenAsync(Garden garden);


        public Task<Garden> GetGardenByIdAsync(int id);

        public Task UpdateGardenAsync(Garden garden);
        public Task DeleteGardenAsync(int id);

    }
}
