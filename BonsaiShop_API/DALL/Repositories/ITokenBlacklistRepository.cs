namespace BonsaiShop_API.DALL.Repositories
{
    public interface ITokenBlacklistRepository
    {
        Task AddTokenToBlacklistAsync(string token, DateTime expiration);
        Task<bool> IsTokenBlacklistedAsync(string token);
    }
}
