using BonsaiShop_API.DALL.Repositories;
using StackExchange.Redis;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class RedisTokenBlacklistRepository : ITokenBlacklistRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisTokenBlacklistRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task AddTokenToBlacklistAsync(string token, DateTime expiration)
        {
            var db = _redis.GetDatabase();
            var expiryTime = expiration - DateTime.UtcNow;

            // Lưu token vào Redis với thời gian hết hạn dựa trên thời gian còn lại của token
            await db.StringSetAsync(token, "blacklisted", expiryTime);
        }

        public async Task<bool> IsTokenBlacklistedAsync(string token)
        {
            var db = _redis.GetDatabase();
            return await db.KeyExistsAsync(token);
        }
    }
}
