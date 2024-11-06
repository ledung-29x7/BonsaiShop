using System.ComponentModel.DataAnnotations;

namespace BonsaiShop_API.Areas.Garden.Models
{
    public class Plants
    {
        private int plantId;
        private int categoryId;
        private int gardenId;
        private string plantName;
        private decimal price;
        private bool isAailable;
        private string description;
        private DateTime createdAt;

        [Key]
        public int PlantId { get => plantId; set => plantId = value; }
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public int GardenId { get => gardenId; set => gardenId = value; }
        public string PlantName { get => plantName; set => plantName = value; }
        public decimal Price { get => price; set => price = value; }
        public bool IsAailable { get => isAailable; set => isAailable = value; }
        public string Description { get => description; set => description = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
    }
}
