namespace BonsaiShop_API.Areas.Garden.Models
{
    public class PlantImageDTO
    {
        public int PlantImageId { get; set; }
        public string ImageBase64 { get; set; }
        public string Caption { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime UploadedAt { get; set; }

        
    }
}
