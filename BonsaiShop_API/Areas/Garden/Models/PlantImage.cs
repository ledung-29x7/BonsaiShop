using System;
using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.Garden.Models
{
    public class PlantImage
    {
        [Key]
        public int PlantImageId { get; set; }
        public int PlantId { get; set; }
        public string ImageBase64 { get; set; }

        [MaxLength(255)]
        public string Caption { get; set; }
        public bool IsPrimary { get; set; } = false;
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        //public Plants Plants { get; set; }
    }
}
