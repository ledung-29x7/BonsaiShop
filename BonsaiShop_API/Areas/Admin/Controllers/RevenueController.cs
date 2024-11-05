using BonsaiShop_API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly BonsaiDbcontext _context;

        public RevenueController(BonsaiDbcontext context)
        {
            _context = context;
        }
        [HttpGet("reports")]
        public IActionResult GetRevenueReport([FromQuery] string Time)
        {
            DateTime startDate;
            DateTime endDate = DateTime.Now;

            switch (Time.ToLower())
            {
                case "day":
                    startDate = endDate.Date;
                    break;
                case "month":
                    startDate = new DateTime(endDate.Year, endDate.Month, 1);
                    break;
                case "year":
                    startDate = new DateTime(endDate.Year, 1, 1);
                    break;
                default:
                    return BadRequest("dữ liệu nhập vào phải là day or month or year");
            }

            var totalRevenue = _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .Sum(o => o.TotalAmount);

            return Ok(new { Time = Time, TotalRevenue = totalRevenue, StartDate = startDate, EndDate = endDate });
        }

        // GET /api/revenue/reports/{id} - Lấy chi tiết báo cáo doanh thu theo ID (admin)
        [HttpGet("reports/{id}")]
        public IActionResult GetRevenueDetail(int id)
        {
            var order = _context.Orders
                .Where(o => o.OrderId == id)
                .Select(o => new
                {
                    o.OrderId,
                    o.TotalAmount,
                    o.OrderDate,
                    o.Status,
                    Customer = o.CustomerId,
                    Details = o.OrderDetails.Select(d => new
                    {
                        d.PlantId,
                        d.Quantity,
                        d.UnitPrice,
                        d.DepositAmount
                    }).ToList()
                })
                .FirstOrDefault();

            if (order == null)
                return NotFound("Order not found.");

            return Ok(order);
        }
    }
}
