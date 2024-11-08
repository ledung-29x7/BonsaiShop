using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.Admin.Models
{
    public class Categories
    {
        private int categoryId;
        private string categoryName;
        private string description;

        [Key]
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public string Description { get => description; set => description = value; }
    }
}
