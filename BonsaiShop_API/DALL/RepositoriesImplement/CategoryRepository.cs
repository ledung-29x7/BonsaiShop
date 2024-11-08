using AutoMapper;
using BonsaiShop_API.Areas.Admin.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static StackExchange.Redis.Role;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BonsaiDbcontext _dbcontext;
        private readonly IMapper _mapper;

        public CategoryRepository(BonsaiDbcontext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }


        public async Task<Categories> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Categories>(categoryDto);

            var nameParam = new SqlParameter("@CategoryName", category.CategoryName);
            var descParam = new SqlParameter("@Description", category.Description ?? (object)DBNull.Value);

            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC CreateCategory @CategoryName, @Description",
                nameParam, descParam);
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var idParam = new SqlParameter("@CategoryId", categoryId);
            var result = await _dbcontext.Database.ExecuteSqlRawAsync("EXEC DeleteCategory @CategoryId", idParam);
            return result > 0;
        }

        public async Task<List<Categories>> GetCategoriesAsync()
        {
            return await _dbcontext.Categories
            .FromSqlRaw("EXEC GetCategories")
            .ToListAsync();
        }

        public async Task<Categories> GetCategoryByIdAsync(int categoryId)
        {
            var categorys = await _dbcontext.Categories
            .FromSqlRaw("EXEC GetCategoryById @CategoryId", new SqlParameter("@CategoryId", categoryId))
            .ToListAsync();
            return categorys.FirstOrDefault();
        }

        public async Task<Categories> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto)
        {
            var category = _mapper.Map<Categories>(categoryDto);
            category.CategoryId = categoryId;

            var idParam = new SqlParameter("@CategoryId", categoryId);
            var nameParam = new SqlParameter("@CategoryName", category.CategoryName);
            var descParam = new SqlParameter("@Description", category.Description ?? (object)DBNull.Value);

            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC UpdateCategory @CategoryId, @CategoryName, @Description",
                idParam, nameParam, descParam);

            return category;
        }
    }
}
