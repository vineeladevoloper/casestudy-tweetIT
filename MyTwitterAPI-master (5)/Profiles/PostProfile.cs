using AutoMapper;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;

namespace MyTwitterAPI.Profiles
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
            CreateMap<Post, PostWithoutIdDTO>();
            CreateMap<PostWithoutIdDTO, Post>();
        }
    }
}
