using AutoMapper;
using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class GardenRepository : IGardenRepository
    {
        private readonly IMapper _mapper;
        private readonly BonsaiDbcontext _dbcontext;
        public GardenRepository(IMapper mapper, BonsaiDbcontext dbcontext) 
        {
            _dbcontext = dbcontext;
            _mapper= mapper;
        }
        public async Task<Gardens> CreateGardenAsync(GardenCreateDto gardenCreateDto)
        {
            var garden = _mapper.Map<Gardens>(gardenCreateDto);

            var nameParam = new SqlParameter("@GardenName", garden.GardenName);
            var addressParam = new SqlParameter("@Address", garden.Address ?? (object)DBNull.Value);
            var phoneParam = new SqlParameter("@Phone", garden.Phone ?? (object)DBNull.Value);
            var descParam = new SqlParameter("@Description", garden.Description ?? (object)DBNull.Value);
            var ownerIdParam = new SqlParameter("@GardenOwnerId", garden.GardenOwnerId);

            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC CreateGarden @GardenName, @Address, @Phone, @Description, @GardenOwnerId",
                nameParam, addressParam, phoneParam, descParam, ownerIdParam);

            return garden;
        }

        public async Task<bool> DeleteGardenAsync(int gardenId)
        {
            var idParam = new SqlParameter("@GardenId", gardenId);
            var result = await _dbcontext.Database.ExecuteSqlRawAsync("EXEC DeleteGarden @GardenId", idParam);
            return result > 0;
        }

        public async Task<List<Gardens>> GetGardensAsync()
        {
            return await _dbcontext.Gardens
            .FromSqlRaw("EXEC GetGardens")
            .ToListAsync();
        }

        public async Task<Gardens> UpdateGardenAsync(int gardenId, GardenCreateDto gardenCreateDto)
        {
            var garden = _mapper.Map<Gardens>(gardenCreateDto);
            garden.GardenId = gardenId;

            var idParam = new SqlParameter("@GardenId", gardenId);
            var nameParam = new SqlParameter("@GardenName", garden.GardenName);
            var addressParam = new SqlParameter("@Address", garden.Address ?? (object)DBNull.Value);
            var phoneParam = new SqlParameter("@Phone", garden.Phone ?? (object)DBNull.Value);
            var descParam = new SqlParameter("@Description", garden.Description ?? (object)DBNull.Value);

            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC UpdateGarden @GardenId, @GardenName, @Address, @Phone, @Description",
                idParam, nameParam, addressParam, phoneParam, descParam);

            return garden;
        }
    }
}
