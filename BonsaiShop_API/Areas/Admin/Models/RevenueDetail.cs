namespace BonsaiShop_API.Areas.Admin.Models
{
    public class RevenueDetail
    {
        public int RevenueDetailId { get; set; }
        public int ReportId { get; set; }
        public int OrderId { get; set; }
        public decimal Revenue { get; set; }
        public RevenueReport RevenueReport { get; set; }
    }
}
