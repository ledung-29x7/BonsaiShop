namespace BonsaiShop_API.Areas.Garden.Models
{
    public class PlantWithImageDto
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }   
        public string ImageBase64 { get; set; }   
    }
}
