using AutoMapper;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;

namespace MyTwitterAPI.Profiles
{
    public class NotificationProfile:Profile
    {
        public NotificationProfile()
        {
            CreateMap<CommentNotification, NotificationWithOutIDDTO>();
            CreateMap<NotificationWithOutIDDTO, CommentNotification>();
            //CreateMap<CommentNotification, NotificationDTO>();
            CreateMap<NotificationDTO, CommentNotification>();
            CreateMap<CommentNotification, NotificationDTO>()
            .ForMember(dest => dest.senderName, opt => opt.MapFrom(src => src.Sender.Name))
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.Name))
            .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post.Title));
        }
    }
}
