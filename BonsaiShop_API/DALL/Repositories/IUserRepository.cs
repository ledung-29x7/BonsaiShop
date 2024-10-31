using BonsaiShop_API.Areas.Auther.Model;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task AddUserAsync(User user);
        Task<User> GetUserById(int id);

        Task UpdateUser(User user);
    }
}
