namespace BonsaiShop_API.Areas.Garden.Models
{
    public class PlantDetailDTO
    {
        public int PlantId { get; set; }
        public int CategoryId { get; set; }
        public int GardenId { get; set; }
        public string PlantName { get; set; }
        public decimal Price { get; set; }
        public bool? IsAvailable { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PlantImageDTO> Images { get; set; }
    }
}
