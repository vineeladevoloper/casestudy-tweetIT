using AutoMapper;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;

namespace MyTwitterAPI.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile() 
        {
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<Comment, CommentWithoutIdDTO>();
            CreateMap<CommentWithoutIdDTO, Comment>();
        }
    }
}
