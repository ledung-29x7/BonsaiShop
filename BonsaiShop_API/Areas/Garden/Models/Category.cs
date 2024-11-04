using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.Garden.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
