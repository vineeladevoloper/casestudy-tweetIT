using AutoMapper;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;

namespace MyTwitterAPI.Profiles
{
    public class LikeProfile:Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, LikeDTO>();
            CreateMap<LikeDTO, Like>();
            CreateMap<Like, LikeWithoutIdDTO>();
            CreateMap<LikeWithoutIdDTO, Like>();
        }
    }
}
