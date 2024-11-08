using AutoMapper;
using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.MappingProfile
{
    public class ImageProfile:Profile
    {
        public ImageProfile()
        {
            CreateMap<GardenImageDTO, GardenImages>();
            CreateMap<PlantImageDTO, PlantImages>();
        }
    }
}
