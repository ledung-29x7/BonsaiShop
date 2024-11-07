using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.Garden.Models
{
    public class PlantImages
    {
        private int plantImageId;
        private int plantId;
        private string imageBase64;
        private string caption;
        private bool isPrimary;
        private DateTime uploadedAt;

        [Key]
        public int PlantImageId { get => plantImageId; set => plantImageId = value; }
        public int PlantId { get => plantId; set => plantId = value; }
        public string ImageBase64 { get => imageBase64; set => imageBase64 = value; }
        public string Caption { get => caption; set => caption = value; }
        public bool IsPrimary { get => isPrimary; set => isPrimary = value; }
        public DateTime UploadedAt { get => uploadedAt; set => uploadedAt = value; }

        public Plants Plant { get; set; }
    }
}
