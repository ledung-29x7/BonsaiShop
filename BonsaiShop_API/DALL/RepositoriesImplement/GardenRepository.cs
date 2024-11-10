using BonsaiShop_API.Areas.Garden.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BonsaiShop_API.DALL.Repositories
{
    public class GardenRepository : IGardenRepository
    {
        private readonly BonsaiDbcontext _context;

        public GardenRepository(BonsaiDbcontext context)
        {
            _context = context;
        }

        public async Task<int> AddGarden(Garden garden)
        {
            var gardenIdParam = new SqlParameter("@GardenId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spAddGarden @GardenOwnerId, @GardenName, @Address, @Phone, @Description",
                new SqlParameter("@GardenOwnerId", garden.GardenOwnerId),
                new SqlParameter("@GardenName", garden.GardenName),
                new SqlParameter("@Address", garden.Address),
                new SqlParameter("@Phone", garden.Phone),
                new SqlParameter("@Description", garden.Description)
            );

            return (int)gardenIdParam.Value;
        }

        public async Task<List<Garden>> GetGardens()
        {
            return await _context.Gardens.FromSqlRaw("EXEC spGetGardens").ToListAsync();
        }

        public async Task UpdateGarden(Garden garden)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spUpdateGarden @GardenId, @GardenName, @Address, @Phone, @Description",
                new SqlParameter("@GardenId", garden.GardenId),
                new SqlParameter("@GardenName", garden.GardenName),
                new SqlParameter("@Address", garden.Address),
                new SqlParameter("@Phone", garden.Phone),
                new SqlParameter("@Description", garden.Description)
            );
        }

        public async Task DeleteGarden(int gardenId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spDeleteGarden @GardenId",
                new SqlParameter("@GardenId", gardenId)
            );
        }
    }
}
