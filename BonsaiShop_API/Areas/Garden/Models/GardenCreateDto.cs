namespace BonsaiShop_API.Areas.Garden.Models
{
    public class GardenCreateDto
    {
        public string GardenName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public int GardenOwnerId { get; set; }
    }
}
