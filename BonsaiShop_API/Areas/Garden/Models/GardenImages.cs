using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.Garden.Models
{
    public class GardenImages
    {
        [Key]
        public int GardenImageId{ get; set; }
         
        public int GardenId { get; set; }
        public string ImageBase64 { get  ; set; }
        public string Caption { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;

    }
}
