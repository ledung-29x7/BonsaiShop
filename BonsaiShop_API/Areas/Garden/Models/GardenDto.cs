namespace BonsaiShop_API.Areas.Garden.Models
{
    public class GardenDto
    {
        public int GardenId { get; set; }
        public string GardenName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
