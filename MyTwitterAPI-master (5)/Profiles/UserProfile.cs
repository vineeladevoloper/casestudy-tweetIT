using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using AutoMapper;

namespace MyTwitterAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<UserwithPWDDTO, User>();
            CreateMap<User, UserwithPWDDTO>();
        }
    }
}
