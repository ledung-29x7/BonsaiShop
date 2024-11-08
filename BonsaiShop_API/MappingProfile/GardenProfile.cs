using AutoMapper;
using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.MappingProfile
{
    public class GardenProfile:Profile
    {
        public GardenProfile()
        {
            CreateMap<Gardens, GardenDto>();
            CreateMap<GardenCreateDto, Gardens>();
        }
    }
}
