using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.Garden.Models
{
    public class GardenImages
    {
        private int gardenImageId;
        private int gardenId;
        private string imageBase64;
        private string caption;
        private bool isPrimary;
        private DateTime uploadedAt;

        [Key]
        public int GardenImageId { get => gardenImageId; set => gardenImageId = value; }
        public int GardenId { get => gardenId; set => gardenId = value; }
        public string ImageBase64 { get => imageBase64; set => imageBase64 = value; }
        public string Caption { get => caption; set => caption = value; }
        public bool IsPrimary { get => isPrimary; set => isPrimary = value; }
        public DateTime UploadedAt { get => uploadedAt; set => uploadedAt = value; }
    }
}
