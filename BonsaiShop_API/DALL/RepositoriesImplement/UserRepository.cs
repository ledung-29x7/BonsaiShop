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

        public async Task ChangePassword(int userId, string oldPassword, string newPasswordHash)
        {
            // Lấy PasswordHash hiện tại từ cơ sở dữ liệu
            var user = await _dbcontext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // So sánh mật khẩu người dùng nhập vào với mật khẩu đã băm trong cơ sở dữ liệu
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
            {
                throw new Exception("Old password does not match.");
            }

            // Nếu mật khẩu cũ khớp, gọi stored procedure để cập nhật mật khẩu mới
            var id_param = new SqlParameter("@UserId", userId);
            var newPasswordHash_param = new SqlParameter("@NewPasswordHash", newPasswordHash);

            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC dbo.ChangePassword @UserId, @NewPasswordHash", id_param, newPasswordHash_param);
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
            var id_param = new SqlParameter("@UserId", user.UserId);
            var username_param = new SqlParameter("@UserName", user.UserName);
            var email_param = new SqlParameter("@Email", user.Email);
            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateUser @UserId, @UserName, @Email, @Role", id_param, username_param, email_param);
        }

        public async Task UpdateUserRole(User user)
        {
            var id_param = new SqlParameter("@UserId", user.UserId);
            var role_param = new SqlParameter("@Role", user.Role);
            await _dbcontext.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateUserRole @UserId, @Role", id_param, role_param);
        }
    }
}
