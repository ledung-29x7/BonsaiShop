namespace BonsaiShop_API.Areas.Garden.Models
{
    public class PlantDTO
    {
        public int CategoryId { get; set; }  // ID loại cây cảnh
        public int GardenId { get; set; }    // ID nhà vườn
        public string PlantName { get; set; } // Tên cây cảnh
        public decimal Price { get; set; }    // Giá
        public string Description { get; set; } // Mô tả
        public bool? IsAvailable { get; set; }
    }
}
