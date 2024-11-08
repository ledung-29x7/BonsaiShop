using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class GardenReponsitory : IGardensReponsitory
    {
        private readonly BonsaiDbcontext _context;

        public GardenReponsitory(BonsaiDbcontext context)
        {
            _context = context;
        }

        public async Task<Garden> CreateGardenAsync(Garden garden)
        {
            var result = await _context.Gardens.FromSqlRaw(
                "EXEC AddGarden @GardenOwnerId, @GardenName, @Address, @Phone, @Description",
                new SqlParameter("@GardenOwnerId", garden.GardenOwnerId),
                new SqlParameter("@GardenName", garden.GardenName),
                new SqlParameter("@Address", garden.Address),
                new SqlParameter("@Phone", garden.Phone),
                new SqlParameter("@Description", garden.Description)
            ).ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Garden>> GetAllGardensAsync()
        {
            return await _context.Gardens.FromSqlRaw("EXEC GetAllGardens").ToListAsync();
        }

        public async Task<Garden> GetGardenByIdAsync(int id)
        {
            var result = await _context.Gardens.FromSqlRaw(
                "EXEC GetGardenById @GardenId",
                new SqlParameter("@GardenId", id)
            ).ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task UpdateGardenAsync(Garden garden)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateGarden @GardenId, @GardenName, @Address, @Phone, @Description",
                new SqlParameter("@GardenId", garden.GardenId),
                new SqlParameter("@GardenName", garden.GardenName),
                new SqlParameter("@Address", garden.Address),
                new SqlParameter("@Phone", garden.Phone),
                new SqlParameter("@Description", garden.Description)
            );
        }

        public async Task DeleteGardenAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteGarden @GardenId",
                new SqlParameter("@GardenId", id)
            );
        }
    }
}
