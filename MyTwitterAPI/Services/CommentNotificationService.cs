using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTwitterAPI.Database;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Model;

namespace MyTwitterAPI.Services
{
    public class CommentNotificationService:ICommentNotificationService
    {
        private readonly MyContext context;
        private readonly IMapper _mapper;

        public CommentNotificationService(MyContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }

        public void AddCommentNotification(CommentNotification notify)
        {
                context.CommentNotifications.Add(notify);
                context.SaveChanges();
         }

        List<NotificationDTO> ICommentNotificationService.GetAllCommentNotifications()
        {
            try
            {
                var notifications = context.CommentNotifications
                    .Include(c => c.Sender)
                    .Include(c => c.Receiver)
                    .Include(c => c.Post)
                    .Where(c => c.Post.Active == 1)  // Add this condition to filter based on Active attribute
                    .ToList();

                return _mapper.Map<List<NotificationDTO>>(notifications);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<NotificationDTO> GetCommentNotificationsByUser(string userId)
        {
            try
            {
                var notifications = context.CommentNotifications
                     .Include(c => c.Sender)
                     .Include(c => c.Receiver)
                     .Include(c => c.Post)
                     .Where(c => c.ReceiverId == userId && c.Post.Active == 1)
                     .ToList();
                return _mapper.Map<List<NotificationDTO>>(notifications);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void MarkAsRead(int commentNotificationId)
        {
            try
            {
                var notification = context.CommentNotifications.Find(commentNotificationId);
                    notification.Read = 1;
                    context.SaveChanges();
             }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
