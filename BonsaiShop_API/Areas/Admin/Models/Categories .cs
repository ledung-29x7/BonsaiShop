using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.Admin.Models
{
    public class Categories
    {
        
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
