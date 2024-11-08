using AutoMapper;
using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.MappingProfile
{
    public class PlantProfile : Profile
    {
        public PlantProfile()
        {
            CreateMap<Plants, PlantDTO>().ReverseMap();
            CreateMap<Plants, PlantDetailDTO>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
            CreateMap<PlantImages, PlantImageDTO>();
        }
    }
}
