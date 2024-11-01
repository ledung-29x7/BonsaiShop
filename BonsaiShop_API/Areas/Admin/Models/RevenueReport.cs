namespace BonsaiShop_API.Areas.Admin.Models
{
    public class RevenueReport
    {
        public int ReportId { get; set; }
        public DateTime ReportDate { get; set; }
        public ICollection<RevenueDetail> RevenueDetails { get; set; }
    }
}
