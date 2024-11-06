using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BonsaiShop_API.Areas.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderReponsitory _orderRepository;

        public OrderDetailController(IOrderReponsitory orderRepository)
        {
            _orderRepository = orderRepository;
        }
        // GET: api/<OrderDetailController>
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var orderDetails = await _orderRepository.GetOrderDetailsAsync(orderId);

            if (orderDetails == null || !orderDetails.Any())
            {
                return NotFound(new { Message = "No order details found." });
            }

            return Ok(orderDetails);
        }
    }
}
