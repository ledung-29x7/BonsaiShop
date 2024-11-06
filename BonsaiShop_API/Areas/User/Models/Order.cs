using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.User.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
