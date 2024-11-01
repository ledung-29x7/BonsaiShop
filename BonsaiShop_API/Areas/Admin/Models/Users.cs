using System.ComponentModel.DataAnnotations;

public class Users
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }  // Stored securely
    public string Role { get; set; }  // E.g., "admin"
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}