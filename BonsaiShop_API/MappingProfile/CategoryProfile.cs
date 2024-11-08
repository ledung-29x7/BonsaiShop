using AutoMapper;
using BonsaiShop_API.Areas.Admin.Models;

namespace BonsaiShop_API.MappingProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Categories, CategoryDto>().ReverseMap();
        }

    }
}
