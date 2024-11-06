using BonsaiShop_API.Areas.User.Models;
using BonsaiShop_API.DALL.Repositories;
using BonsaiShop_API.DALL.RepositoriesImplement;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BonsaiShop_API.Areas.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderReponsitory _orderRepository;

        public OrderController(IOrderReponsitory orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders(int? orderId)
        {
            var orders = await _orderRepository.GetOrdersAsync(orderId);

            if (orders == null || !orders.Any())
            {
                return NotFound(new { Message = "No orders found." });
            }

            return Ok(orders);
        } 
    
            // POST api/order
            [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null || order.OrderDetails == null || !order.OrderDetails.Any())
            {
                return BadRequest("Order or Order details are missing.");
            }

            var resultMessage = await _orderRepository.CreateOrderAsync(order);

            return Ok(new { Message = resultMessage });
        }
      

    }
}
