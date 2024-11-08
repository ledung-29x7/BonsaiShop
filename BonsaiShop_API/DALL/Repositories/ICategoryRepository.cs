using BonsaiShop_API.Areas.Admin.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface ICategoryRepository
    {
        Task<Categories> CreateCategoryAsync(CategoryDto categoryDto);
        Task<List<Categories>> GetCategoriesAsync();
        Task<Categories> GetCategoryByIdAsync(int categoryId);
        Task<Categories> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
