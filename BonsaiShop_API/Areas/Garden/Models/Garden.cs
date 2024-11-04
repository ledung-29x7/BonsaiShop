using System;
using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.Garden.Models
{
    public class Garden
    {
        [Key]
        public int GardenId { get; set; }
        public int GardenOwnerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string GardenName { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
