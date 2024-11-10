namespace BonsaiShop_API.Areas.Garden.Models
{
    public class Garden
    {
        public int GardenId { get; set; }
        public int GardenOwnerId { get; set; }
        public string GardenName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
