namespace BonsaiShop_API.Areas.Auther.Model
{
    public class User
    {
        private int userId;
        private string userName;
        private string passwordHash;
        private string email;
        private string role;
        private DateTime createdAt;

        public int UserId { get => userId; set => userId = value; }
        public string UserName { get => userName; set => userName = value; }
        public string PasswordHash { get => passwordHash; set => passwordHash = value; }
        public string Email { get => email; set => email = value; }
        public string Role { get => role; set => role = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
    }
}
