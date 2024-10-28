using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol.Plugins;

namespace BonsaiShop_API.Areas.Garden.Models
{
    public class Plant
    {
        private int plantId;
        private int categoryId;
        private int gardenId;
        private string plantName;
        private decimal price;
        private bool isAvailable;
        private string description;
        private DateTime createdAt = DateTime.Now;

        public int PlantId { get => plantId; set => plantId = value; }
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public int GardenId { get => gardenId; set => gardenId = value; }
        public string PlantName { get => plantName; set => plantName = value; }
        public decimal Price { get => price; set => price = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public string Description { get => description; set => description = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
    }
}
