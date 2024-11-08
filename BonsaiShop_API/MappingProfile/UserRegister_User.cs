using AutoMapper;
using BonsaiShop_API.Areas.Auther.Model.Dtos;
using BonsaiShop_API.Areas.Auther.Model;

namespace BonsaiShop_API.MappingProfile
{
    public class UserRegister_User : Profile
    {
        public UserRegister_User() 
        {
            CreateMap<UserRegisterDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
