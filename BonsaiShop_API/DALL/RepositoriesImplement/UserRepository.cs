using BonsaiShop_API.Areas.Auther.Model;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Linq;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class UserRepository : IUserRepository
    {
        private readonly BonsaiDbcontext _dbcontext;

        public UserRepository(BonsaiDbcontext dbcontext) 
        {
            _dbcontext = dbcontext;
        }
        public async Task AddUserAsync(User user)
        {
            var username_param = new SqlParameter("@UserName", user.UserName);
            var email_param = new SqlParameter("@Email", user.Email);
            var PasswordHash_param = new SqlParameter("@PasswordHash", user.PasswordHash);
            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC dbo.AddUser @UserName, @PasswordHash, @Email", username_param, PasswordHash_param, email_param);

        }

        public async Task<User> GetUserByEmail(string email)
        {
            var email_param = new SqlParameter("@Email",email);
            var users =  await _dbcontext.Users.FromSqlRaw("EXECUTE dbo.spGetUserByEmail @Email", email_param).ToListAsync();
            return users.FirstOrDefault();
        }

        public async Task<User> GetUserById(int id)
        {
            var id_param = new SqlParameter("@UserId", id);
            var users = await _dbcontext.Users.FromSqlRaw("EXECUTE dbo.GetUserById @UserId", id_param).ToListAsync();
            return users.FirstOrDefault();
        }

        public async Task UpdateUser(User user)
        {
            var username_param = new SqlParameter("@UserName", user.UserName);
            var email_param = new SqlParameter("@Email", user.Email);
            var role_param = new SqlParameter("@Role", user.Role);
            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateUser @UserName, @Email, @Role", username_param, email_param, role_param);
        }
    }
}
