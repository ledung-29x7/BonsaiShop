using BonsaiShop_API.Areas.User.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IOrderReponsitory
    {
        Task<string> CreateOrderAsync(Order order);
           Task<List<Order>> GetOrdersAsync(int? orderId = null);
        Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId);
    }
}
